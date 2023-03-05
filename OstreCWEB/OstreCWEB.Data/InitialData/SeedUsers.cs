using Microsoft.AspNetCore.Identity;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.Repository.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Repository.InitialData
{
    internal class SeedUsers
    {
        public static async Task<List<User>> Seed(OstreCWebContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            string asdminRole = "admin";
            string memberRole = "user";
            string userPassword = "User12!@";
            string adminPassword = "Admin12!@";
            var admin = new User
            {
                UserName = "AdminUser",
                Name = "AdminUser",
                Email = "aa@aa.aa",
                PhoneNumber = "6902341234",
                EmailConfirmed = true
            };
            var user = new User
            {
                UserName = "TestUser",
                Name = "TestUser",
                Email = "test@test.com",
                PhoneNumber = "1112223333",
                EmailConfirmed = true

            };

            if (await roleManager.FindByNameAsync(asdminRole) == null)
            {
              
                await roleManager.CreateAsync(new IdentityRole<int>
                {
                    Name =asdminRole,
                    NormalizedName = asdminRole.ToUpper()
                   
                });
            }

            if (await roleManager.FindByNameAsync(memberRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>
                {
                    Name = memberRole,
                    NormalizedName = memberRole.ToUpper()

                });
            }

            if (await userManager.FindByNameAsync("AdminUser") == null)
            {
                var result = await userManager.CreateAsync(admin);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(admin, adminPassword);
                    await userManager.AddToRoleAsync(admin, asdminRole);
                }
            }

            if (await userManager.FindByNameAsync("TestUser") == null)
            {
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, userPassword);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }
            var users = new List<User>();
            users.Add(admin);
            users.Add(user);
            return users;
        }
    }
}
