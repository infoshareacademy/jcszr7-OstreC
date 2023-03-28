using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.StoryModels;

namespace OstreCWEB.Repository.Repository.StoryRepo
{
    public interface IParagraphRepository<T> : IEntityBaseRepo<Paragraph> where T : Paragraph
    {
        public Task<IQueryable<Paragraph>> GetPaginatedListAsync(int idStory);
    }
}