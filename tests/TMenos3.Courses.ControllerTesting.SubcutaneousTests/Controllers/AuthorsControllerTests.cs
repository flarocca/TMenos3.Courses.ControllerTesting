using AutoMapper.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMenos3.Courses.ControllerTesting.API;
using TMenos3.Courses.ControllerTesting.Contracts.Dtos.Requests;
using TMenos3.Courses.ControllerTesting.Contracts.Dtos.Responses;
using TMenos3.Courses.ControllerTesting.Persistance;
using Xunit;

namespace TMenos3.Courses.ControllerTesting.SubcutaneousTests.Controllers
{
    public class AuthorsControllerTests
    {
        private readonly HttpClient _client;

        public AuthorsControllerTests()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost");
        }

        [Fact]
        public async Task Post_AuthorRequest_StatusCodeIsCreated()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task Post_AuthorRequest_ResponseContainsExpectedAuthorDto()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task Post_AuthorRequest_ResponseContainsCopyrightAndLocationHeaders()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
