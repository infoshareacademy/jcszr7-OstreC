namespace OstreCWEB.Services.Game
{
    public interface IGameService
    {
        public Task<UserParagraph> CreateNewGameInstanceAsync(string userId, int characterTemplateId, int storyId);
        public Task DeleteGameInstanceAsync(int userParagrahId);
        public Task SetActiveGameInstanceAsync(int userParagraphId, string userId);
        public Task NextParagraphAsync(string userId, int choiceId);
        public Task HealCharacterAsync(string userId);
        public Task<int> TestThrowAsync(string userId, int rollValue, int modifire);
        public Task<int[]> ThrowDice(int maxValue, string userId);
        public Task NextParagraphAfterFightAsync(UserParagraph gameInstance, int choiceId);
        public Task UnequipItemAsync(int itemId, string userId);
        public Task EquipItemAsync(int itemId, string userId);

    }
}
