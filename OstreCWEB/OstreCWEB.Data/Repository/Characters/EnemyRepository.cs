using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;

#nullable disable

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class EnemyRepository : IEnemyRepository
    {
        private readonly OstreCWebContext _context;

        public EnemyRepository(OstreCWebContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Enemy item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Enemy item)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Enemy>> GetAllTemplatesAsync()
        {
            return _context.Enemies
                .Where(e => e.IsTemplate)
                .ToList();
        }

        public async Task<Enemy> GetByIdAsync(int id)
        {
            return await _context.Enemies
                .Include(e => e.LinkedItems)
                .Include(e => e.LinkedAbilities)
                .SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Enemy> GetByIdNoTrackingAsync(int id)
        {
            return await _context.Enemies
                .Include(e => e.LinkedItems)
                .Include(e => e.LinkedAbilities)
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);
        }
        public async Task UpdateAsync(Enemy item)
        {
            throw new NotImplementedException();
        }
    }
}
