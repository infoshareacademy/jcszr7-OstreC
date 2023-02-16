using OstreCWEB.DomainModels.Identity;

namespace OstreCWEB.Data.Repository.Identity
{
    public interface IIdentityRepository
    {
        public Task AddUser(User user);
        public Task<User> GetUser(string id);
        public Task<List<User>> GetAll();
        public Task Update(User user);
    }
}
