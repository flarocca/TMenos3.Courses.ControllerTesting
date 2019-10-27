using System;
using TMenos3.Courses.ControllerTesting.Models.Enums;

namespace TMenos3.Courses.ControllerTesting.Models.Entities
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
