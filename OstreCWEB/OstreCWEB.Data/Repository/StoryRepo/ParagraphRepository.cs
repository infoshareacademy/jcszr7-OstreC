using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.Repository.DataBase;

namespace OstreCWEB.Repository.Repository.StoryRepo
{
    internal class ParagraphRepository : EntityBaseRepo<Paragraph>, IParagraphRepository<Paragraph>
    {
        private readonly OstreCWebContext _context;

        public ParagraphRepository(OstreCWebContext context) : base(context)
        {
            _context = context;
        }
    }
}