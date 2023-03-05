using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Services.Game
{
    public interface IGameService
    {
        public Task<UserParagraph> CreateNewGameInstanceAsync(int userId, int characterTemplateId, int storyId);
        public Task DeleteGameInstanceAsync(int userParagrahId);
        public Task SetActiveGameInstanceAsync(int userParagraphId, int userId);
        public Task NextParagraphAsync(int userId, int choiceId);
        public Task HealCharacterAsync(int userId);
        public Task<int> TestThrowAsync(int userId, int rollValue, int modifire);
        public Task<int[]> ThrowDice(int maxValue, int userId);
        public Task NextParagraphAfterFightAsync(UserParagraph gameInstance, int choiceId);
        public Task UnequipItemAsync(int itemId, int userId);
        public Task EquipItemAsync(int itemId, int userId);

    }
}
