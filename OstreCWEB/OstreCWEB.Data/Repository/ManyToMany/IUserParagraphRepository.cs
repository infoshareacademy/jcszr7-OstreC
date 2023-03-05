﻿using OstreCWeb.DomainModels;
using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Repository.Repository.ManyToMany
{
    public interface IUserParagraphRepository<T> :IEntityBaseRepo<T> where T : class
    {
        public Task<UserParagraph> Add();
       
        public Task Create(UserParagraph newGameSession);
        public Task UpdateAsync(UserParagraph gameSession);
        public Task Delete(UserParagraph gameSession);
        public Task<UserParagraph> GetActiveByUserIdAsync(int userId);
        public Task<UserParagraph> GetByUserParagraphIdAsync(int userParagraphId);
        public Task<UserParagraph> GetActiveByUserIdNoTrackingAsync(int userId);
        public UserParagraph GetActiveByUserIdNoTracking(int userId);
        public Task SaveChangesAsync();
        public Task DeleteInstanceBasedOnRace(int id);
        public Task DeleteInstanceBasedOnClass(int id);
    }
}
