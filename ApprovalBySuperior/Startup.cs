using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApprovalBySuperior.Services;
using AspNetCore.RouteAnalyzer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;

namespace ApprovalBySuperior
{
    public class Startup
    {

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

            services.AddControllersWithViews();

            // Add Injection of RepositoryClass by sim
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();


            // Add ApprovalBySuperiro To Sqlite by sim
            /*services.AddDbContext<MyContext>(options =>
                                              options.UseSqlite
                                              (Configuration.GetConnectionString("MyContext")));*/

            // Add ApprovalBySuperior To PostgreSQL by sim
            // なんかよくわからんけど、.NETCore6からappsettingsファイルで指定ができなくなった。。
            services.AddDbContext<MyContext>(options =>
                                             options.UseNpgsql
                                             ("Host=localhost; Database=aprvsupe; Username=sim; Password=omniversal;"));

            // Add ApprovalBySuperior To PostgreSQL-AWSRDS by sim
            /*services.AddDbContext<MyContext>(options =>
                                 options.UseNpgsql
                                 ("Host=aprvsupe.cliw44fstoz7.ap-northeast-1.rds.amazonaws.com; Database=aprvsupe; Username=sim; Password=omniversal;"));*/

            // Add ApprovalBySuperior To Oracle by sim
            /*services.AddDbContext<MyContext>(options =>
                                             options.UseOracle
                                             (Configuration.GetConnectionString("MyContext")));*/

            services.AddRouteAnalyzer();
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseCors();
            app.UseCookiePolicy();
            app.UseRouting();
            //以下はリバースプロキシ を挟む場合、クライアント情報が欠損することを回避するためのコード
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            // .netcore6（厳密には3?)からの書き方
            app.UseEndpoints(routes =>
            { 
                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Users}/{action=Index}/{id?}"
                    );

                routes.MapControllerRoute(
                    name: "Position",
                    pattern: "{controller=Positions}/{action=Index}/{id?}"
                    );

                routes.MapControllerRoute(
                    name: "Department",
                    pattern: "{controller=Departments}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
