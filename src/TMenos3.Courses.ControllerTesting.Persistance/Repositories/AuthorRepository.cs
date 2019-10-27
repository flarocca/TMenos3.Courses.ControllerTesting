using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMenos3.Courses.ControllerTesting.Models.Entities;

namespace TMenos3.Courses.ControllerTesting.Persistance.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ControllerTestingDbContext _dbContext;

        public AuthorRepository(ControllerTestingDbContext dbContext) 
            => _dbContext = dbContext;

        public async Task<Author> CreateAsync(Author author)
        {
            _dbContext.Authors.Add(author);
            await _dbContext.SaveChangesAsync();
            return author;
        }

        public async Task<IEnumerable<Author>> GetAllAsync() => 
            await _dbContext.Authors
                .Include(author => author.Books)
                .ToListAsync();

        public async Task<Author> GetAsync(Guid id) =>
            await _dbContext.Authors
                .Include(author => author.Books)
                .Where(author => author.Id == id)
                .FirstOrDefaultAsync();
    }
}
