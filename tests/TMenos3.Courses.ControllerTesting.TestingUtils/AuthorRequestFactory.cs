using System;
using TMenos3.Courses.ControllerTesting.Contracts.Dtos.Requests;

namespace TMenos3.Courses.ControllerTesting.TestingUtils
{
    public static class AuthorRequestFactory
    {
        public static AuthorRequest Create(string firstName, string lastName, DateTime dateOfBirth) =>
            new AuthorRequest
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };
    }
}
