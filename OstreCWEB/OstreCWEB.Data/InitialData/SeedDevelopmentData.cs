using Microsoft.AspNetCore.Identity;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.Repository.DataBase;
namespace OstreCWEB.Repository.InitialData
{
    public class SeedDevelopmentData
    {
        public static async Task Initialize(
        OstreCWebContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager)
        { 
                var users = await SeedUsers.Seed(context, userManager, roleManager);
                SeedCharacters.Seed(context, users);
                SeedStories.Seed(context, users.FirstOrDefault(u => u.UserName == "AdminUser")); 
           
        } 
    }
}