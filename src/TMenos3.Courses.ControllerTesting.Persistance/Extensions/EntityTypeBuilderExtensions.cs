using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TMenos3.Courses.ControllerTesting.Models.Entities;

namespace TMenos3.Courses.ControllerTesting.Persistance.Extensions
{
    internal static class EntityTypeBuilderExtensions
    {
        public static void SetupBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : Entity
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
