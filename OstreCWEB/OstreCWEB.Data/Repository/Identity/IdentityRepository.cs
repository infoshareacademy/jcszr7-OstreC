using Microsoft.EntityFrameworkCore;
using OstreCWEB.Data.DataBase;
using OstreCWEB.Data.Factory;
using OstreCWEB.Data.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.Identity;

namespace OstreCWEB.Data.Repository.Identity
{

    internal class IdentityRepository : IIdentityRepository
    {
        private readonly ICharacterFactory _playableCharacterFactory;
        private readonly IPlayableCharacterRepository _playableCharacterRepository;
        private OstreCWebContext _context { get; }
        public IdentityRepository(OstreCWebContext context, IPlayableCharacterRepository playableCharacterRepository, ICharacterFactory playableCharacterFactory)
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
        public async Task<User> GetUser(string id)
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
