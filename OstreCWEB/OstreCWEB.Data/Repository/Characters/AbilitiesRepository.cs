using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class AbilitiesRepository : IAbilitiesRepository
    {
        private OstreCWebContext _db;
        public AbilitiesRepository(OstreCWebContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Ability characterAction)
        {
            _db.CharacterActions.Add(characterAction);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _db.CharacterActions.Remove(GetByIdWithLinkedItemsAsync(id));
            await _db.SaveChangesAsync();
        }

        public async Task<List<Ability>> GetAllAsync()
        {
            return await _db.CharacterActions
                .Include(s => s.Status)
                .Include(s => s.LinkedCharacter)
                .ToListAsync();
        }

        public async Task<Ability> GetByIdAsync(int id)
        {
            return await _db.CharacterActions
                .Include(s => s.Status)
                .Include(s => s.LinkedCharacter)
                .SingleOrDefaultAsync(s => s.Id == id);
        }
        private Ability GetByIdWithLinkedItemsAsync(int id)
        {
            return _db.CharacterActions
                .Include(s => s.Status)
                .Include(s => s.LinkedCharacter)
                .Include(x => x.LinkedItems)
                .SingleOrDefault(s => s.Id == id);
        }

        public async Task UpdateAsync(Ability characterAction)
        {
            _db.CharacterActions.Update(characterAction);
            await _db.SaveChangesAsync();
        }
    }
}
