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
        public override async Task DeleteAsync(int id)
        {
            var entity = await base.GetByIdAsync(id, x => x.PlayableClass, x=>x.Ability);
            _context.Items.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
