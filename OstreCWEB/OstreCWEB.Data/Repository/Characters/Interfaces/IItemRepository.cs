using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters.Interfaces
{
    public interface IItemRepository<T> : IEntityBaseRepo<T> where T:class
    {  
        public Task<IQueryable<Item>> GetPaginatedListAsync();
    }
}
