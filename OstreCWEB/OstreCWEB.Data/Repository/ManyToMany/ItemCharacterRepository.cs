using OstreCWEB.Data.DataBase;
using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Data.Repository.ManyToMany
{
    internal class ItemCharacterRepository : IItemCharacterRepository
    {
        public OstreCWebContext _context { get; }

        public ItemCharacterRepository(OstreCWebContext context)
        {
            _context = context;
        }

        public async Task AddRange(List<ItemCharacter> itemsCharacter)
        {
            _context.ItemsCharactersRelation.AddRange(itemsCharacter);
            await _context.SaveChangesAsync();
        }

        public Task<ItemCharacter> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<ItemCharacter> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ItemCharacter> GetByCharacterId()
        {
            throw new NotImplementedException();
        }

        public Task<ItemCharacter> Update()
        {
            throw new NotImplementedException();
        }

    }
}
