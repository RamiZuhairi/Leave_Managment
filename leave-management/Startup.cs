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
using leave_management.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using leave_management.Contracts;
using leave_management.Repostitory;
using AutoMapper;
using leave_management.Mappings;

namespace leave_management
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //Add refrences for Repository and Contracts to startup file
            // add the knowlage and perpuses of out classes to dot net core ,its important to let .NetCore the relationsip between the tables 
            services.AddScoped<ILeaveTypeRepostitory, LeaveTypeRepostitory>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

            // now after we have dont the Mapping we have add new service to inisialise it here when the app is runnning 
            services.AddAutoMapper(typeof(Maps));// we called our class  Mapper 




            //services.AddDefaultIdentity<Employee>(options => options.SignIn.RequireConfirmedAccount = true)// this is means when someone registoers must have conformation email, but we have to change that in register.cs
            services.AddDefaultIdentity<Employee>()// we will disable email conformation for now  look above
                .AddRoles<IdentityRole>()// we going to identify the Roles  for the user 
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<Employee> userManager, RoleManager<IdentityRole>roleManager )
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

            //add seed metod, as we see its static very easy to call  
            SeedData.Seed(userManager, roleManager);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
