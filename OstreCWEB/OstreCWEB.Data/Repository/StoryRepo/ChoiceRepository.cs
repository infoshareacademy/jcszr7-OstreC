using OstreCWEB.DomainModels.StoryModels.Properties;
using OstreCWEB.Repository.DataBase;

namespace OstreCWEB.Repository.Repository.StoryRepo
{
    internal class ChoiceRepository : EntityBaseRepo<Choice>, IChoiceRepository<Choice>
    {
        private readonly OstreCWebContext _context;

        public ChoiceRepository(OstreCWebContext context) : base(context)
        {
            _context = context;
        }
    }
}