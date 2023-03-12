using OstreCWEB.DomainModels.StoryModels;

namespace OstreCWEB.Repository.Repository.StoryRepo
{
    public interface IParagraphRepository<T> : IEntityBaseRepo<Paragraph> where T : Paragraph
    {
    }
}