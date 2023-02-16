using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class CharacterRaceRepository : ICharacterRaceRepository
    {
        private readonly OstreCWebContext _ostreCWebContext;
        public CharacterRaceRepository(OstreCWebContext ostreCWebContext)
        {
            _ostreCWebContext = ostreCWebContext;
        }

        public async Task CreateAsync(PlayableRace item)
        {
            _ostreCWebContext.PlayableCharacterRaces.Add(item);
            await _ostreCWebContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _ostreCWebContext.PlayableCharacterRaces.Remove(await GetByIdAsync(id));
            await _ostreCWebContext.SaveChangesAsync();
        }

        public async Task<List<PlayableRace>> GetAllAsync()
        {
            return await _ostreCWebContext.PlayableCharacterRaces.ToListAsync();
        }

        public async Task<PlayableRace> GetByIdAsync(int id)
        {
            return await _ostreCWebContext.PlayableCharacterRaces.SingleOrDefaultAsync(x => x.PlayableRaceId == id);
        }
        public PlayableRace GetById(int id)
        {
            return _ostreCWebContext.PlayableCharacterRaces.SingleOrDefault(x => x.PlayableRaceId == id);
        }

        public async Task UpdateAsync(PlayableRace item)
        {
            _ostreCWebContext.Update(item);
            await _ostreCWebContext.SaveChangesAsync();
        }
    }
}
