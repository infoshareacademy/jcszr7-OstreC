using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.DomainModels.StoryModels.Properties;

namespace OstreCWEB.Repository.Repository.StoryRepo
{
    public interface IStoryRepository<T> : IEntityBaseRepo<Story> where T : Story
    {
        public Task<IReadOnlyCollection<Story>> GetAllStories();

        public Task<IReadOnlyCollection<Story>> GetStoriesByUserId(int userId);

        public Task<Story> GetStoryByIdAsync(int idStory);

        public Story GetStoryById(int idStory);

        public Task<Story> GetStoryWithParagraphsById(int idStory);

        public Task<Paragraph> GetParagraphById(int idParagraph);

        public Task<Paragraph> GetCombatParagraphById(int idParagraph);

        public Task<Story> GetStoryNoIncludesAsync(int storyId);

        public Task UpdateStory(Story story);

        public Task AddStory(Story story);

        public Task DeleteStory(Story story);

        public Task AddParagraph(Paragraph paragraph);

        public Task DeleteParagraph(Paragraph paragraph);

        public Task<Paragraph> GetParagraphToEditById(int paragraphId);

        public Task UpdateParagraph(Paragraph paragraph);

        public Task AddEnemyToParagraph(EnemyInParagraph enemyInParagraph);

        public Task DeleteEnemyInParagraph(int enemyInParagraphId);

        public Task<Choice> GetChoiceDetailsById(int idChoice);

        public Task AddChoice(Choice choice);

        public Task DeleteChoice(int choiceId);

        public Task UpdateChoice(Choice choice);

        public bool Exists(int story);
    }
}