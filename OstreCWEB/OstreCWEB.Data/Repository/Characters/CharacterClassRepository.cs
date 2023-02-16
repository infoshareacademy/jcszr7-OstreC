using Microsoft.EntityFrameworkCore;
using OstreCWEB.Data.DataBase;
using OstreCWEB.Data.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Data.Repository.Characters
{
    internal class CharacterClassRepository : ICharacterClassRepository
    {
        OstreCWebContext _context;
        public CharacterClassRepository(OstreCWebContext context)
        {
            _context = context;
        }

        public OstreCWebContext Context { get; }

        public async Task CreateAsync(PlayableClass item)
        {
            _context.PlayableCharacterClasses.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var playableClass = await GetByIdAsync(id);
            _context.PlayableCharacterClasses.Remove(playableClass);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PlayableClass>> GetAllAsync()
        {
            return await _context.PlayableCharacterClasses.ToListAsync();
        }

        public async Task<PlayableClass> GetByIdNoIncludesAsync(int id)
        {
            return await _context.PlayableCharacterClasses
                .SingleOrDefaultAsync(x => x.PlayableClassId == id);
        }
        private async Task<PlayableClass> GetByIdAsync(int id)
        {
            return await _context.PlayableCharacterClasses
                .Include(x => x.ItemsGrantedByClass)
                .Include(x => x.ActionsGrantedByClass)
                .SingleOrDefaultAsync(x => x.PlayableClassId == id);
        }
        public PlayableClass GetById(int id)
        {
            return _context.PlayableCharacterClasses.SingleOrDefault(x => x.PlayableClassId == id);
        }

        public async Task UpdateAsync(PlayableClass item)
        {
            _context.PlayableCharacterClasses.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
