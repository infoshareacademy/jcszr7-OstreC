using OstreCWEB.DomainModels.StoryModels.Properties;

namespace OstreCWEB.Repository.Repository.StoryRepo
{
    public interface IChoiceRepository<T> : IEntityBaseRepo<Choice> where T : Choice
    {
    }
}