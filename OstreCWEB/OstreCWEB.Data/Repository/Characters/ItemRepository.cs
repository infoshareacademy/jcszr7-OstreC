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


        public async Task CreateAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Items.Remove(await GetByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Item>> GetAllAsync()
        {
            return await _context.Items
                .Include(i => i.ActionToTrigger)
                .Include(i => i.PlayableClass)
                .ToListAsync();
        }
         

        public async Task UpdateAsync(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }
        public async Task<IQueryable<Item>> GetPaginatedListAsync()
        {
            return _context.Items.AsNoTracking();
        }
    }
}
