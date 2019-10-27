using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMenos3.Courses.ControllerTesting.Models.Entities;

namespace TMenos3.Courses.ControllerTesting.Persistance.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();

        Task<Author> GetAsync(Guid id);

        Task<Author> CreateAsync(Author author);
    }
}
