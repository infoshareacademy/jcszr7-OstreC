using OstreCWEB.Repository.Repository.Identity;
using OstreCWEB.DomainModels.Identity;
using System.Security.Claims;

namespace OstreCWEB.Services.Identity
{
    internal class UserService : IUserService
    {
        private readonly IIdentityRepository _identityRepository;
        public UserService(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _identityRepository.GetUser(id);
        }
        public async Task<User> GetUserByIdForNewGameInstance(string id)
        {
            return await _identityRepository.GetUser(id);
        }

        public string GetUserId(ClaimsPrincipal user)
        {
            if (user == null) { return ""; }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null) { return ""; }

            return userId.Value;

        }
    }
}
