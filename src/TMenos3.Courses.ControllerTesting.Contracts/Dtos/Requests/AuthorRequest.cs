using System;

namespace TMenos3.Courses.ControllerTesting.Contracts.Dtos.Requests
{
    public class AuthorRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
