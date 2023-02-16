using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Data.Repository.Characters.Interfaces
{
    public interface IAbilitiesRepository
    {
        public Task<Abilities> GetByIdAsync(int id);
        public Task<List<Abilities>> GetAllAsync();
        public Task UpdateAsync(Abilities item);
        public Task CreateAsync(Abilities item);
        public Task DeleteAsync(int id);
    }
}
