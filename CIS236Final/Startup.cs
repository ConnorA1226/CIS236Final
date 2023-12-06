using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CIS236Final.Models;

namespace CIS236Final
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });

            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CIS236Final")));
        }

        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{searchString?}");

                endpoints.MapControllerRoute(
                    name: "customRouteWithVariable",
                    pattern: "custom/{variable}",
                    defaults: new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "customRouteWithoutVariable",
                    pattern: "custom",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}