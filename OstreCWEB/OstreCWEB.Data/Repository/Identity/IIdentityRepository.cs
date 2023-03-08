using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Identity;

namespace OstreCWEB.Repository.Repository.Identity
{
    public interface IIdentityRepository<T> : IEntityBaseRepo<User> where T : User
    { 
        public Task<User> GetUserGameStart(int id); 
    }
}
