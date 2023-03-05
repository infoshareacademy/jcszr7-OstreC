using OstreCWEB.DomainModels.Identity;
using System.Security.Claims;

namespace OstreCWEB.Services.Identity
{
    public interface IUserService
    {
        public int GetUserId(ClaimsPrincipal user);
        public Task<User> GetUserById(int id);
        public Task<List<User>> GetAllUsers();
    }
}
