using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using LPGCylinderSystem.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LPGCylinderSystem.Data.Stores;
using MongoDB.Bson;
using LPGCylinderSystem.Models;


namespace LPGCylinderSystem
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
            //If we are using MYSQL 
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser,ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultUI()
                 .AddDefaultTokenProviders()
                .AddRoleStore<MongoRoleStore<ApplicationRole, ObjectId>>()
                .AddUserStore<MongoUserStore<ApplicationUser,ObjectId>>();
            //.AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
            });
            
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<MongoTablesFactory>();
            services.AddScoped<ApplicationRole>();
            services.AddScoped<ClassRepository<ApplicationUser>>(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>

            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                name: "Complaint",
                pattern: "{controller=Complaint}/{action=Resolve}/{id?}/{uid?}");

                endpoints.MapControllerRoute(
                name: "Complaint",
                pattern: "{controller=Complaint}/{action=Index}/{id?}");


                endpoints.MapRazorPages();
            });
        }
    }
}
