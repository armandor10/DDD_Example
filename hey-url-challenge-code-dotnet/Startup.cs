using System.Security.Policy;
using hey_url_challenge_code_dotnet.Commons.Repositories;
using hey_url_challenge_code_dotnet.Infra.Data.Repositories;
using hey_url_challenge_code_dotnet.Infra.DataContract;
using hey_url_challenge_code_dotnet.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using hey_url_challenge_code_dotnet.Infra.Data;

namespace HeyUrlChallengeCodeDotnet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBrowserDetection();
            services.AddControllersWithViews();
            services.AddDbContext<hey_url_challenge_code_dotnet.Infra.Data.ApplicationContext>(options => options.UseInMemoryDatabase(databaseName: "HeyUrl"));
            services.AddUrlModule();
            // Repositories
            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<IVisitsRepository, VisitsRepository>();
            services.AddScoped<IVisitsBinnacleRepository, VisitsBinnacleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork<hey_url_challenge_code_dotnet.Infra.Data.ApplicationContext>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<hey_url_challenge_code_dotnet.Infra.Data.ApplicationContext>();
            context.Database.EnsureCreated();
        }
    }
}
