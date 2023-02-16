using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Data.Repository.Characters.Interfaces
{
    public interface ICharacterClassRepository
    {
        public Task<PlayableClass> GetByIdNoIncludesAsync(int id);
        public Task<List<PlayableClass>> GetAllAsync();
        public Task UpdateAsync(PlayableClass item);
        public Task CreateAsync(PlayableClass item);
        public Task DeleteAsync(int id);
        public PlayableClass GetById(int id);
    }
}
