using Microsoft.EntityFrameworkCore;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class PlayableCharacterRepository : EntityBaseRepo<PlayableCharacter>, IPlayableCharacterRepository<PlayableCharacter>
    {
        private OstreCWebContext _db;

        public PlayableCharacterRepository(OstreCWebContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<PlayableCharacter>> GetAllTemplatesAsync()
        {
            return await _db.PlayableCharacters.Where(x => x.IsTemplate).ToListAsync();
        }

        /// <summary>
        /// Gets all playable characters except those belonging to a given user.
        /// </summary>
        /// <param name="userCharacters"></param>
        /// <returns></returns>
        public async Task<List<PlayableCharacter>> GetAllTemplatesExceptAsync(int userId)
        {
            return await _db.PlayableCharacters.Where(c => c.UserId != userId && c.IsTemplate == true).ToListAsync();
        }

        public async Task<PlayableCharacter> GetByIdNoTrackingAsync(int characterTemplateId)
        {
            return await _db.PlayableCharacters
                 .Include(x => x.CharacterClass)
                 .ThenInclude(y => y.ActionsGrantedByClass)
                 .Include(x => x.CharacterClass)
                 .ThenInclude(y => y.ItemsGrantedByClass)
                 .AsNoTracking()
                 .SingleOrDefaultAsync(x => x.Id == characterTemplateId);
        }
    }
}