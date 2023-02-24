using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters.Interfaces
{
    public interface IAbilitiesRepository
    {
        public Task<Ability> GetByIdAsync(int id);
        public Task<List<Ability>> GetAllAsync();
        public Task UpdateAsync(Ability item);
        public Task CreateAsync(Ability item);
        public Task DeleteAsync(int id);
    }
}
