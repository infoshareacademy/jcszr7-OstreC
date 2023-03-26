using Microsoft.EntityFrameworkCore;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Factory;
using OstreCWEB.Repository.Repository.Characters.Interfaces;

namespace OstreCWEB.Repository.Repository.Identity
{
    internal class IdentityRepository : EntityBaseRepo<User>, IIdentityRepository<User>
    {
        private readonly ICharacterFactory _playableCharacterFactory;
        private readonly IPlayableCharacterRepository<PlayableCharacter> _playableCharacterRepository;
        private OstreCWebContext _context { get; }

        public IdentityRepository(OstreCWebContext context, IPlayableCharacterRepository<PlayableCharacter> playableCharacterRepository, ICharacterFactory playableCharacterFactory) : base(context)
        {
            _playableCharacterFactory = playableCharacterFactory;
            _playableCharacterRepository = playableCharacterRepository;
            _context = context;
        }

        public async Task<User> GetUserByIdForLobbyAsync(int id)
        {
            return await _context.Users
               .Include(x => x.UserParagraphs) 
               .SingleOrDefaultAsync(x=>x.Id ==id);
        }
        public async Task<User> GetUserGameStart(int id)
        {
            var user = await _context.Users
                .Include(x => x.UserParagraphs)
                .Include(x => x.StoriesCreated)
                .ThenInclude(x => x.Paragraphs)
                .SingleOrDefaultAsync(u => u.Id == id);
            return user;
        }
    }
}