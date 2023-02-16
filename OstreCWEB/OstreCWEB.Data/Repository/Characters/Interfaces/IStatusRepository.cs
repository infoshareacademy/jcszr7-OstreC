using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Data.Repository.Characters.Interfaces
{
    public interface IStatusRepository
    {
        public Task<Status> GetByIdAsync(int id);
        public Task<List<Status>> GetAllAsync();
        public Task UpdateAsync(Status Status);
        public Task CreateAsync(Status Status);
        public Task DeleteAsync(Status Status);
    }
}
