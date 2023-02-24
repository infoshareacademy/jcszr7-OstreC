using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.Services.StoryServices.Models;

namespace OstreCWEB.Services.StoryServices
{
    public interface IStoryService
    {
        //Tools
        public Task<IReadOnlyCollection<Story>> GetAllStories();
        public Task<IReadOnlyCollection<Story>> GetStoriesByUserId(string userId);
        public Task<Story> GetStoryByIdAsync(int idStory);
        public Story GetStoryById(int idStory);
        public Task<Story> GetStoryWithParagraphsById(int idStory);

        //Story
        public Task UpdateStory(int idStory, string Name, string Description, string userId);
        public Task AddStory(Story story, string userId);
        public Task DeleteStory(int idStory, string userId);

        //Paragraph
        public Task<Paragraph> GetParagraphById(int idParagraphId);
        public Task<ParagraphDetails> GetParagraphDetailsById(int idParagraph, int idStory);
        public Task AddParagraph(Paragraph paragraph, string userId);
        public Task<IReadOnlyCollection<Enemy>> GetAllEnemies();
        public Task DeleteParagraph(int idParagraph, string userId);
        public Task<EditParagraph> GetEditParagraphById(int paragraphId);
        public Task UpdateParagraph(EditParagraph editParagraph, string userId);
        public Task AddEnemyToParagraph(EnemyInParagraphService enemyInParagraphService);
        public Task DeleteEnemyInParagraph(int enemyInParagraphId);

        //Choice
        public Task<ChoiceDetails> GetChoiceDetailsById(int idChoice);
        public Task<ChoiceCreator> GetChoiceCreator(int firstParagraphId, int secondParagraphId);
        public Task<ChoiceCreator> GetChoiceCreatorById(int choiceId);
        public Task<ChoiceCreator> GetChoiceCreatorById(int choiceId, int secondParagraphId);
        public Task AddChoice(ChoiceCreator choiceCreator);
        public Task DeleteChoice(int choiceId);
        public Task UpdateChoice(ChoiceCreator choiceCreator);

        public bool Exists(int storyId);
    }
}
