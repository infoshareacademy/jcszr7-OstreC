using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.Repository.DataBase;

namespace SeedIdentity.Data
{
    public class SeedDevelopmentData
    {
        public static async Task Initialize(OstreCWebContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        {

            context.Database.EnsureCreated();

            string asdminRole = "admin";
            string memberRole = "user";
            string userPassword = "User12!@";
            string adminPassword = "Admin12!@";


            if (await roleManager.FindByNameAsync(asdminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(asdminRole));
            }

            if (await roleManager.FindByNameAsync(memberRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(memberRole));
            }

            if (await userManager.FindByNameAsync("AdminUser") == null)
            {
                var user = new User
                {
                    UserName = "AdminUser",
                    Name = "AdminUser",
                    Email = "aa@aa.aa",
                    PhoneNumber = "6902341234"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, adminPassword);
                    await userManager.AddToRoleAsync(user, asdminRole);
                }
            }

            if (await userManager.FindByNameAsync("TestUser") == null)
            {
                var user = new User
                {
                    UserName = "TestUser",
                    Name = "TestUser",
                    Email = "test@test.com",
                    PhoneNumber = "1112223333"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, userPassword);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }
        }
    }
}