using OstreCWEB.Repository.Repository.Identity;
using OstreCWEB.DomainModels.Identity;
using System.Security.Claims;

namespace OstreCWEB.Services.Identity
{
    internal class UserService : IUserService
    {
        private readonly IIdentityRepository<User> _identityRepository;
        public UserService(IIdentityRepository<User> identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _identityRepository.GetUserGameStart(id);
        }
        public async Task<User> GetUserByIdForNewGameInstance(int id)
        {
            return await _identityRepository.GetUserGameStart(id);
        }

        public int GetUserId(ClaimsPrincipal user)
        {
            if (user == null) { return 0; }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null) { return 0; }

            int.TryParse(userId.Value, out var result);
            return result;

        }
    }
}
