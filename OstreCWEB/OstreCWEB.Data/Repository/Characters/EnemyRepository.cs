using Microsoft.EntityFrameworkCore;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;

#nullable disable

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class EnemyRepository : EntityBaseRepo<Enemy>, IEnemyRepository<Enemy>
    {
        private readonly OstreCWebContext _context;

        public EnemyRepository(OstreCWebContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Enemy>> GetAllTemplatesAsync()
        {
            return _context.Enemies
                .Where(e => e.IsTemplate)
                .ToList();
        }
    }
}