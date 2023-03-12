using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.Services.StoryBuilder.Models;
using OstreCWEB.Services.StoryBuilder.ModelsDto;

namespace OstreCWEB.Services.StoryBuilder
{
    public interface IStoryBuilderServices
    {
        //Tools
        public Task<List<Story>> GetAllStories();

        public Task<List<StoryView>> GetStoriesByUserId(int userId);

        public Task<StoryView> GetStoryByIdAsync(int idStory);

        public Story GetStoryById(int idStory);

        public Task<StoryParagraphsView> GetStoryWithParagraphsById(int idStory);

        //Story
        public Task UpdateStory(StoryView story, int userId);

        public Task AddStory(StoryView newStory, int userId);

        public Task DeleteStory(int idStory, int userId);

        //Paragraph
        public Task<Paragraph> GetParagraphById(int idParagraphId);

        public Task<ParagraphDetailsView> GetParagraphDetailsById(int idParagraph, int idStory);

        public Task AddParagraph(Paragraph paragraph, int userId);

        public Task<List<Enemy>> GetAllEnemies();

        public Task<List<Item>> GetAllItems();

        public Task DeleteParagraph(int idParagraph, int userId);

        public Task<EditParagraphView> GetEditParagraphById(int paragraphId);

        public Task UpdateParagraph(EditParagraph editParagraph, int userId);

        public Task AddEnemyToParagraph(EnemyInParagraphService enemyInParagraphService);

        public Task DeleteEnemyInParagraph(int enemyInParagraphId);

        //Choice
        public Task<ChoiceDetails> GetChoiceDetailsById(int idChoice);

        public Task<ChoiceCreatorView> GetChoiceCreator(int firstParagraphId, int secondParagraphId);

        public Task<ChoiceCreatorView> GetChoiceCreatorById(int choiceId);

        public Task<ChoiceCreatorView> GetChoiceCreatorById(int choiceId, int secondParagraphId);

        public Task AddChoice(ChoiceCreatorView choiceCreator);

        public Task DeleteChoice(int choiceId);

        public Task UpdateChoice(ChoiceCreatorView choiceCreator);

        public bool Exists(int storyId);
    }
}