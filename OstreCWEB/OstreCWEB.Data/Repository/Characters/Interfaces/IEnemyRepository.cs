using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters.Interfaces
{
    public interface IEnemyRepository
    {
        public Task<Enemy> GetByIdAsync(int id);
        public Task<IReadOnlyCollection<Enemy>> GetAllTemplatesAsync();
        public Task<Enemy> GetByIdNoTrackingAsync(int id);
        public Task UpdateAsync(Enemy item);
        public Task CreateAsync(Enemy item);
        public Task DeleteAsync(Enemy item);
    }
}
