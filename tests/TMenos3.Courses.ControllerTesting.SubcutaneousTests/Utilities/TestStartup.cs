using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMenos3.Courses.ControllerTesting.API;
using TMenos3.Courses.ControllerTesting.API.Filters;
using TMenos3.Courses.ControllerTesting.API.Infrastructure;
using TMenos3.Courses.ControllerTesting.TestingUtils;

namespace TMenos3.Courses.ControllerTesting.SubcutaneousTests.Utilities
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) 
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddControllers(options =>
            {
                options.Filters.Add<CopyRightActionFilter>();
            });
            services.AddInMemoryDatabase();
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);

            services.AddSingleton<CopyRightActionFilter>();
        }
    }
}
