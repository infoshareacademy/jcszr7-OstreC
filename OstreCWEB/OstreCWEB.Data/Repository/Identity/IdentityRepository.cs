using Microsoft.EntityFrameworkCore;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Factory;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.Identity;

namespace OstreCWEB.Repository.Repository.Identity
{

    internal class IdentityRepository : IIdentityRepository
    {
        private readonly ICharacterFactory _playableCharacterFactory;
        private readonly IPlayableCharacterRepository<PlayableCharacter> _playableCharacterRepository;
        private OstreCWebContext _context { get; }
        public IdentityRepository(OstreCWebContext context, IPlayableCharacterRepository<PlayableCharacter> playableCharacterRepository, ICharacterFactory playableCharacterFactory)
        {
            _playableCharacterFactory = playableCharacterFactory;
            _playableCharacterRepository = playableCharacterRepository;
            _context = context;
        }
        public Task AddUser(User user)
        {
            _context.Add(user);
            return Task.CompletedTask;
        }
        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
                .Include(x => x.UserParagraphs)
                .Include(x => x.StoriesCreated)
                .ThenInclude(x => x.Paragraphs)
                .SingleOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task Update(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}
