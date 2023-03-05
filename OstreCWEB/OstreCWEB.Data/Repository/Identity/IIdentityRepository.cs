using OstreCWEB.DomainModels.Identity;

namespace OstreCWEB.Repository.Repository.Identity
{
    public interface IIdentityRepository
    {
        public Task AddUser(User user);
        public Task<User> GetUser(int id);
        public Task<List<User>> GetAll();
        public Task Update(User user);
    }
}
