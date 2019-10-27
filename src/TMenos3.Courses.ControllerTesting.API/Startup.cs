using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TMenos3.Courses.ControllerTesting.API.Filters;
using TMenos3.Courses.ControllerTesting.API.Infrastructure;
using TMenos3.Courses.ControllerTesting.Persistance.Repositories;

namespace TMenos3.Courses.ControllerTesting.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load("TMenos3.Courses.ControllerTesting.API"));
            services.AddControllers(options =>
            {
                options.Filters.Add<CopyRightActionFilter>();
            });
            services.AddDatabase(Configuration);
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddSingleton<CopyRightActionFilter>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection()
               .UseRouting()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllers();
               });
        }
    }
}
