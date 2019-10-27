using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TMenos3.Courses.ControllerTesting.Contracts.Dtos.Responses;
using TMenos3.Courses.ControllerTesting.Models.Entities;
using TMenos3.Courses.ControllerTesting.Persistance.Repositories;
using TMenos3.Courses.ControllerTesting.SubcutaneousTests.Utilities;
using TMenos3.Courses.ControllerTesting.TestingUtils;
using Xunit;
using Constants = TMenos3.Courses.ControllerTesting.API.Infrastructure.Constants;

namespace TMenos3.Courses.ControllerTesting.SubcutaneousTests.Controllers
{
    public class AuthorsControllerTests
    {
        private const string _baseUrl = "http://localhost";

        private readonly HttpClient _client;
        private readonly IAuthorRepository _authorRepositoryMock;

        public AuthorsControllerTests()
        {
            _authorRepositoryMock = A.Fake<IAuthorRepository>();

            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddScoped(service => _authorRepositoryMock);
                })
                .UseStartup<TestStartup>();

            var testServer = new TestServer(builder);
            _client = testServer.CreateClient();
            _client.BaseAddress = new Uri(_baseUrl);
        }

        [Fact]
        public async Task Post_AuthorRequest_StatusCodeIsCreated()
        {
            // Arrange
            var request = AuthorRequestFactory.Create("Mr", "Andersson", DateTime.Now);

            // Act
            using var content = HttpContentFactory.CreateStringContent(request);
            using var response = await _client.PostAsync("api/authors", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Post_AuthorRequest_ResponseContainsExpectedAuthorDto()
        {
            // Arrange
            var expectedId = Guid.NewGuid();
            var expectedAuthor = new Author
            {
                Id = expectedId,
                FirstName = $"Name{expectedId.ToString()}",
                LastName = "author last name",
                DateOfBirth = DateTime.Now
            };
            var request = AuthorRequestFactory.Create(expectedAuthor.FirstName, expectedAuthor.LastName, expectedAuthor.DateOfBirth);
            A.CallTo(() => _authorRepositoryMock.CreateAsync(A<Author>.That.Matches(a => a.FirstName == expectedAuthor.FirstName)))
                .Returns(Task.FromResult(expectedAuthor));

            using var content = HttpContentFactory.CreateStringContent(request);

            // Act
            using var response = await _client.PostAsync("api/authors", content);

            // Assert
            var authorResponse = await response.DeserializeToAsync<AuthorResponse>();

            authorResponse.FirstName.Should().Be(expectedAuthor.FirstName);
            authorResponse.LastName.Should().Be(expectedAuthor.LastName);
            authorResponse.DateOfBirth.Should().Be(expectedAuthor.DateOfBirth);
        }

        [Fact]
        public async Task Post_AuthorRequest_ResponseContainsCopyrightAndLocationHeaders()
        {
            // Arrange
            var expectedId = Guid.NewGuid();
            var expectedLocation = $"{_baseUrl}/api/authors/{expectedId.ToString()}";
            var request = AuthorRequestFactory.Create($"Name{expectedId.ToString()}", "Andersson", DateTime.Now);

            A.CallTo(() => _authorRepositoryMock.CreateAsync(A<Author>.That.Matches(a => a.FirstName == request.FirstName)))
                .Returns(Task.FromResult(new Author { Id = expectedId }));

            using var content = HttpContentFactory.CreateStringContent(request);

            // Act
            using var response = await _client.PostAsync("api/authors", content);

            // Assert
            response.Headers.Location.Should().Be(expectedLocation);
            response.Headers.GetValues(Constants.CopyrightHeader).ElementAt(0).Should().Be(Constants.CopyrightValue);
        }
    }
}
