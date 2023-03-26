using Microsoft.EntityFrameworkCore;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.Repository.DataBase;

namespace OstreCWEB.Repository.Repository.StoryRepo
{
    internal class ParagraphRepository : EntityBaseRepo<Paragraph>, IParagraphRepository<Paragraph>
    {
        private OstreCWebContext _context;
        public ParagraphRepository(OstreCWebContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IQueryable<Paragraph>> GetPaginatedListAsync(int idStory)
        {
            return _context.Paragraphs
                .Where(x => x.StoryId == idStory)
                .Include(x => x.Choices)
                .AsNoTracking();
        }
    }
}