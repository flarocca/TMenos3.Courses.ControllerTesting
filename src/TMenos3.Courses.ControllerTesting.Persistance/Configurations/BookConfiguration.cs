using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMenos3.Courses.ControllerTesting.Models.Entities;
using TMenos3.Courses.ControllerTesting.Persistance.Extensions;

namespace TMenos3.Courses.ControllerTesting.Persistance.Configurations
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.SetupBaseEntity();

            builder.Property(book => book.Title).IsRequired();
            builder.Property(book => book.Description).IsRequired();
            builder.Property(book => book.Genre).IsRequired().HasConversion<string>();

            builder.HasOne(book => book.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.AuthorId);
        }
    }
}
