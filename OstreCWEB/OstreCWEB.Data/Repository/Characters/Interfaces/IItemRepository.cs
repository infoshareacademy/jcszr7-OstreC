using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters.Interfaces
{
    public interface IItemRepository
    {
        public Task<Item> GetByIdAsync(int id);
        public Task<List<Item>> GetAllAsync();
        public Task UpdateAsync(Item item);
        public Task CreateAsync(Item item);
        public Task DeleteAsync(int itemId);
        public Task<IQueryable<Item>> GetPaginatedListAsync();
    }
}
