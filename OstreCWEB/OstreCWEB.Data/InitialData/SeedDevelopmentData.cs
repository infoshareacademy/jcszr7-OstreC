using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.InitialData;
namespace OstreCWEB.Repository.InitialData
{
    public class SeedDevelopmentData
    {
        public static async Task Initialize(OstreCWebContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        { 
                var users = await SeedUsers.Seed(context, userManager, roleManager);
                SeedCharacters.Seed(context, users);
                SeedStories.Seed(context, users.FirstOrDefault(u => u.UserName == "AdminUser")); 
           
        } 
    }
}