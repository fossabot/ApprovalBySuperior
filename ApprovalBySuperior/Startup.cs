using ApprovalBySuperior.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApprovalBySuperior.Services;
using AspNetCore.RouteAnalyzer;
using Microsoft.AspNetCore.HttpOverrides;

namespace ApprovalBySuperior
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add Injection of RepositoryClass by sim
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();


            // Add ApprovalBySuperior To PostgreSQL by sim
            services.AddDbContext<MyContext>(options =>
                                             options.UseNpgsql
                                             (Configuration.GetConnectionString("MyContext")));


            // Add ApprovalBySuperior To Oracle by sim
            //services.AddDbContext<MyContext>(options =>
            //                                 options.UseOracle
            //                                 (Configuration.GetConnectionString("MyContext")));

            // routes analyzer→http://localhost:5001/routes
            services.AddRouteAnalyzer();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Users/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            //以下はリバースプロキシ を挟む場合、クライアント情報が欠損することを回避するためのコード
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseMvc(routes =>
            {
                routes.MapRouteAnalyzer("/routes");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Users}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "Position",
                    template: "{controller=Positions}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "Department",
                    template: "{controller=Departments}/{action=Index}/{id?}");

            });
        }
    }
}
