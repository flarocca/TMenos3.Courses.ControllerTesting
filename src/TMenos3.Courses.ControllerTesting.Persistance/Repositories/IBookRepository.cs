using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMenos3.Courses.ControllerTesting.Models.Entities;

namespace TMenos3.Courses.ControllerTesting.Persistance.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync(Guid authorId);
        Task<Book> GetAsync(Guid authorId, Guid id);
        Task<Book> CreateAsync(Guid authorId, Book book);
    }
}
