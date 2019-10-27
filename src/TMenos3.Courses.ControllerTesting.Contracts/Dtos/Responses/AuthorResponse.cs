using System;

namespace TMenos3.Courses.ControllerTesting.Contracts.Dtos.Responses
{
    public class AuthorResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
