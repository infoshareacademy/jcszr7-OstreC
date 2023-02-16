using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class ItemRepository : IItemRepository
    {
        private OstreCWebContext _context;
        public ItemRepository(OstreCWebContext context)
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

        public async Task<Item> GetByIdAsync(int id)
        {
            return await _context.Items.FirstOrDefaultAsync(x => x.ItemId == id);
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
