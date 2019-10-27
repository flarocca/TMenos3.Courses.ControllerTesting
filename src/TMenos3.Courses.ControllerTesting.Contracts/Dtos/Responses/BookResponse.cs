using System;

namespace TMenos3.Courses.ControllerTesting.Contracts.Dtos.Responses
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public AuthorResponse Author { get; set; }
    }
}
