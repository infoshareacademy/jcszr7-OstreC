using OstreCWEB.Repository.DataBase;
using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Repository.Repository.ManyToMany
{
    internal class ItemCharacterRepository : EntityBaseRepo<ItemCharacter>, IItemCharacterRepository<ItemCharacter>
    {
        public OstreCWebContext _context { get; }

        public ItemCharacterRepository(OstreCWebContext context):base(context)
        {
            _context = context;
        }

        public async Task AddRange(List<ItemCharacter> itemsCharacter)
        {
            _context.ItemsCharactersRelation.AddRange(itemsCharacter);
            await _context.SaveChangesAsync();
        } 
    }
}
