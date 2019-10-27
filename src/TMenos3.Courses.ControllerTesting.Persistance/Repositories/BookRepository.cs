using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMenos3.Courses.ControllerTesting.Models.Entities;

namespace TMenos3.Courses.ControllerTesting.Persistance.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ControllerTestingDbContext _dbContext;

        public BookRepository(ControllerTestingDbContext dbContext) 
            => _dbContext = dbContext;

        public async Task<Book> CreateAsync(Guid authorId, Book book)
        {
            var author = await _dbContext.Authors
                .Where(author => author.Id == authorId)
                .FirstOrDefaultAsync();

            if (author == null)
                throw new ArgumentException("Author does not exist.", nameof(authorId));

            book.AuthorId = author.Id;

            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();

            return book;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(Guid authorId) =>
            await _dbContext.Books
                .Where(book => book.AuthorId == authorId)
                .Include(book => book.Author)
                .ToListAsync();

        public async Task<Book> GetAsync(Guid authorId, Guid id) =>
            await _dbContext.Books
                .Where(book => book.AuthorId == authorId && book.Id == id)
                .Include(book => book.Author)
                .FirstOrDefaultAsync();
    }
}
