using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters.Interfaces
{
    public interface IEnemyRepository<T> : IEntityBaseRepo<Enemy> where T : Enemy
    {
        public Task<List<Enemy>> GetAllTemplatesAsync();
    }
}