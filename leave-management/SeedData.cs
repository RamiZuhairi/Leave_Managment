using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management
{
    public static class SeedData // we make it static to use it without creating object to that class , easier ,but if the class is static must everything inside static to dont forget 
    {

        public static void Seed(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            //dont forget to add Seed that to startup.cs

        }
        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)//if we dont have the first admin role thenw e have to create it 
            {
                var user = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admain@localhost"
                };
                var result = userManager.CreateAsync(user, "Admin@123").Result;//then we have to create the user with username and pasword,must be complex password or give error
                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();// if we added user and everything work we have to add user to administrator
                }

            }

        }
        private static void SeedRoles(RoleManager<IdentityRole> roleManager)//we already need only admain users for manage
        {
            if(!roleManager.RoleExistsAsync("Administrator").Result)//if the admin doent exist so we create new adim when its fresh system 
            {
                var role = new IdentityRole//create new role
                {
                    Name = "Administrator"
                };
           var result= roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Employee").Result)//do same thing for emplyee role 
            {
                var role = new IdentityRole//create new role
                {
                    Name = "Employee"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }

    }
}
