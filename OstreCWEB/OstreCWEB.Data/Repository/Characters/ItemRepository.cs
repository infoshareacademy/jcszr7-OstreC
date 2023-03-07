using Microsoft.EntityFrameworkCore;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class ItemRepository : EntityBaseRepo<Item>, IItemRepository<Item>
    {
        private OstreCWebContext _context;
        public ItemRepository(OstreCWebContext context):base(context)
        {
            _context = context;
        }    
        public async Task<IQueryable<Item>> GetPaginatedListAsync()
        {
            return _context.Items
                .Include(x=>x.PlayableClass)
                .AsNoTracking();
        }
    }
}
