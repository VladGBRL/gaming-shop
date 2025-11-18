using Gaming_Shop.AccountManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.AccountManagement.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var roles = new List<Role>
            {
                new() { Name = "User" },
                new() { Name = "Admin" }
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            var adminUser = new User
            {
                UserName = "admin",
                Email = "admin@gamingshop.com"
            };

            var result = await userManager.CreateAsync(adminUser, "Admin1234!");

            if (result.Succeeded)
            {
                var admin = await userManager.FindByNameAsync("admin");
                if (admin != null)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
