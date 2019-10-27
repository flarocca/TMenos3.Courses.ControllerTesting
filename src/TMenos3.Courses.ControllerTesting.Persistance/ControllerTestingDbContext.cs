using Microsoft.EntityFrameworkCore;
using TMenos3.Courses.ControllerTesting.Models.Entities;
using TMenos3.Courses.ControllerTesting.Persistance.Configurations;

namespace TMenos3.Courses.ControllerTesting.Persistance
{
    public class ControllerTestingDbContext : DbContext
    {
        public ControllerTestingDbContext(DbContextOptions<ControllerTestingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
        }
    }
}
