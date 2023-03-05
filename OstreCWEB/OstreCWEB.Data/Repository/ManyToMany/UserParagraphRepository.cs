using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.Repository.Repository.Identity;
using OstreCWEB.DomainModels.ManyToMany;

#nullable disable

namespace OstreCWEB.Repository.Repository.ManyToMany
{
    internal class UserParagraphRepository : IUserParagraphRepository
    {
        private OstreCWebContext _context;
        private readonly IIdentityRepository _identityRepository;
        private readonly IPlayableCharacterRepository _playableCharacterRepository;
        public UserParagraphRepository(OstreCWebContext context, IIdentityRepository indentityRepository, IPlayableCharacterRepository playableCharacterRepository)
        {
            _context = context;
            _identityRepository = indentityRepository;
            _playableCharacterRepository = playableCharacterRepository;
        }


        public async Task DeleteInstanceBasedOnRace(int raceId)
        {
            var instances = await _context.PlayableCharacters
                .Where(x => x.RaceId == raceId && !x.IsTemplate)
                .Include(x => x.UserParagraph)
                .Select(x => x.UserParagraph)
                .ToListAsync();
            _context.UserParagraphs.RemoveRange(instances);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteInstanceBasedOnClass(int classId)
        {
            var instances = await _context.PlayableCharacters
                .Where(x => x.PlayableClassId == classId && !x.IsTemplate)
                .Include(x => x.UserParagraph)
                .Select(x => x.UserParagraph)
                .ToListAsync();
            _context.UserParagraphs.RemoveRange(instances);
            await _context.SaveChangesAsync();
        }
        public Task<UserParagraph> Add()
        {
            throw new NotImplementedException();
        }
        public async Task Create(UserParagraph newGameSession)
        {
            _context.UserParagraphs.AddAsync(newGameSession);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(UserParagraph gameSession)
        {
            _context.PlayableCharacters.Remove(gameSession.ActiveCharacter);
            _context.UserParagraphs.Remove(gameSession);
            await _context.SaveChangesAsync();
        }
        public async Task<List<UserParagraph>> GetAll()
        {
            return await _context.UserParagraphs.ToListAsync();
        }
        public async Task<UserParagraph> GetById(int id)
        {
            return await _context.UserParagraphs.SingleOrDefaultAsync(c => c.UserParagraphId == id);
        }
        public async Task UpdateAsync(UserParagraph gameSession)
        {
            _context.UserParagraphs.Update(gameSession);
            await _context.SaveChangesAsync();
        }

        public async Task<UserParagraph> GetByUserParagraphIdAsync(int userParagraphId)
        {
            return await _context.UserParagraphs
                .Include(x => x.User)
                .Include(x => x.ActiveCharacter)
                .SingleOrDefaultAsync(u => u.UserParagraphId == userParagraphId);
        }

        public async Task<UserParagraph> GetActiveByUserIdAsync(int userId)
        {
            return await _context.UserParagraphs
                .Include(z => z.User)
                .Include(x => x.Paragraph)
                    .ThenInclude(p => p.Choices)
                .Include(x => x.Paragraph)
                    .ThenInclude(x => x.TestProp)
                .Include(x => x.Paragraph)
                    .ThenInclude(x => x.FightProp)
                    .ThenInclude(y => y.ParagraphEnemies)
                    .ThenInclude(z => z.Enemy)
                .Include(x => x.Paragraph)
                    .ThenInclude(p => p.ParagraphItems)
                        .ThenInclude(pi => pi.Item)
                .Include(x => x.ActiveCharacter)
                .SingleOrDefaultAsync(s => s.User.Id == userId && s.ActiveGame);
        }
        public async Task<UserParagraph> GetActiveByUserIdNoTrackingAsync(int userId)
        {
            var result = await _context.UserParagraphs
                 .Include(x => x.ActiveCharacter)
                 .Include(x => x.Paragraph)
                     .ThenInclude(p => p.Choices)
                 .Include(x => x.Paragraph)
                     .ThenInclude(x => x.TestProp)
                 .Include(x => x.Paragraph)
                     .ThenInclude(x => x.FightProp)
                     .ThenInclude(y => y.ParagraphEnemies)
                     .ThenInclude(z => z.Enemy)
                 .AsNoTracking()
                 .SingleOrDefaultAsync(s => s.User.Id == userId && s.ActiveGame);
            var test = _context.ChangeTracker;
            return result;
        }
        public UserParagraph GetActiveByUserIdNoTracking(int userId)
        {
            var result = _context.UserParagraphs
                 .Include(x => x.Paragraph)
                     .ThenInclude(p => p.Choices)
                 .Include(x => x.Paragraph)
                     .ThenInclude(x => x.TestProp)
                 .Include(x => x.Paragraph)
                     .ThenInclude(x => x.FightProp)
                     .ThenInclude(y => y.ParagraphEnemies)
                     .ThenInclude(z => z.Enemy)
                 .Include(x => x.ActiveCharacter)
                 .AsNoTracking()
                 .SingleOrDefault(s => s.User.Id == userId && s.ActiveGame);
            var test = _context.ChangeTracker;
            return result;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
