using OstreCWeb.DomainModels;
using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Repository.Repository.ManyToMany
{
    public interface IUserParagraphRepository<T> :IEntityBaseRepo<T> where T : class
    { 
        public Task UpdateAsync(UserParagraph gameSession);
        public Task DeleteAsync(UserParagraph gameSession);
        public Task<UserParagraph> GetActiveByUserIdAsync(int userId);
        public Task<UserParagraph> GetByUserParagraphIdAsync(int userParagraphId);
        public Task<UserParagraph> GetActiveByUserIdNoTrackingAsync(int userId); 
        public Task SaveChangesAsync();
        public Task DeleteInstanceBasedOnRace(int id);
        public Task DeleteInstanceBasedOnClass(int id);
    }
}
