using System;
using System.Collections.Generic;

namespace TMenos3.Courses.ControllerTesting.Models.Entities
{
    public class Author : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
