using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TMenos3.Courses.ControllerTesting.Persistance;

namespace TMenos3.Courses.ControllerTesting.TestingUtils
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services)
        {
            services.AddDbContext<ControllerTestingDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDatabase");
            });

            return services;
        }
    }
}
