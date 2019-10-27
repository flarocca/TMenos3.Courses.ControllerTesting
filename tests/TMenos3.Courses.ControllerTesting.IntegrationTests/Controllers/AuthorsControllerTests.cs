using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TMenos3.Courses.ControllerTesting.API;
using TMenos3.Courses.ControllerTesting.Contracts.Dtos.Requests;
using TMenos3.Courses.ControllerTesting.Contracts.Dtos.Responses;
using Xunit;
using System.Linq;
using System.Net;
using Constants = TMenos3.Courses.ControllerTesting.API.Infrastructure.Constants;
using TMenos3.Courses.ControllerTesting.TestingUtils;
using Microsoft.EntityFrameworkCore;
using TMenos3.Courses.ControllerTesting.Persistance;

namespace TMenos3.Courses.ControllerTesting.IntegrationTests.Controllers
{
    public class AuthorsControllerTests
    {
        private const string _baseUrl = "http://localhost";

        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public AuthorsControllerTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(_configuration);

            var testServer = new TestServer(builder);
            _client = testServer.CreateClient();
            _client.BaseAddress = new Uri(_baseUrl);
        }

        [Fact]
        public async Task Post_AuthorRequest_AuthorIsAdded()
        {
            // Arrange
            var request = AuthorRequestFactory.Create("Mr", "Andersson", DateTime.Now);
            using var content = HttpContentFactory.CreateStringContent(request);

            // Act
            using var response = await _client.PostAsync("api/authors", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var authorResponse = await response.DeserializeToAsync<AuthorResponse>();

            authorResponse.FirstName.Should().Be(request.FirstName);
            authorResponse.LastName.Should().Be(request.LastName);
            authorResponse.DateOfBirth.Should().Be(request.DateOfBirth);

            response.Headers.Location.Should().Be($"{_baseUrl}/api/authors/{authorResponse.Id.ToString()}");
            response.Headers.GetValues(Constants.CopyrightHeader).ElementAt(0).Should().Be(Constants.CopyrightValue);

            await EnsureAuthorIsCorrectOnDatabaseAsync(authorResponse);
        }

        private async Task EnsureAuthorIsCorrectOnDatabaseAsync(AuthorResponse authorResponse)
        {
            var options = new DbContextOptionsBuilder<ControllerTestingDbContext>()
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                .Options;
            using var dbContext = new ControllerTestingDbContext(options);

            var dbAuthor = await dbContext.Authors
                .Where(author => author.Id == authorResponse.Id)
                .FirstOrDefaultAsync();

            authorResponse.Id.Should().Be(dbAuthor.Id);
            authorResponse.FirstName.Should().Be(dbAuthor.FirstName);
            authorResponse.LastName.Should().Be(dbAuthor.LastName);
            authorResponse.DateOfBirth.Date.Should().Be(dbAuthor.DateOfBirth.Date);
        }
    }
}
