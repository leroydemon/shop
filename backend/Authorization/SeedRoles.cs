﻿using Authorization.DbLevel.Models;
using DbLevel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Authorization
{
    public class SeedRoles
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<Role>>();
                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<User>>();

                string[] roleNames = { "Admin", "User", "Manager" };

                var powerUser = new User
                {
                    UserName = "Admin",
                    Email = "Admin"
                };

                string userPassword = "AdminAdmin";
                var user = await userManager.FindByEmailAsync("Admin");

                if (user == null)
                {
                    var createPowerUser = await userManager.CreateAsync(powerUser, userPassword);
                    if (createPowerUser.Succeeded)
                    {
                        await userManager.AddToRoleAsync(powerUser, "Admin");
                        await userManager.AddClaimAsync(powerUser, new Claim(ClaimTypes.Role, "Admin"));
                    }
                }
            }
        }
    }
}
