using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMenos3.Courses.ControllerTesting.Models.Entities;
using TMenos3.Courses.ControllerTesting.Persistance.Extensions;

namespace TMenos3.Courses.ControllerTesting.Persistance.Configurations
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.SetupBaseEntity();

            builder.Property(author => author.FirstName).IsRequired();
            builder.Property(author => author.LastName).IsRequired();
            builder.Property(author => author.DateOfBirth).IsRequired();

            builder.HasMany(author => author.Books)
                .WithOne(book => book.Author)
                .HasForeignKey(book => book.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
