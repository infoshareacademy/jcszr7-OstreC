﻿using OstreCWEB.Data.Repository.StoryModels.Properties;

namespace OstreCWEB.Data.Repository.StoryModels
{
    public interface IStoryRepository
    {
        public Task<IReadOnlyCollection<Story>> GetAllStories();
        public Task<IReadOnlyCollection<Story>> GetStoriesByUserId(string userId);
        public Task<Story> GetStoryById(int idStory);
        public Task<Story> GetStoryWithParagraphsById(int idStory);
        public Task<Paragraph> GetParagraphById(int idParagraph); 
        public Task<Paragraph> GetCombatParagraphById(int idParagraph);
        public Task<Story> GetStoryNoIncludesAsync(int storyId);
        public Task UpdateStory(Story story);
        public Task AddStory(Story story);
        public Task DeleteStory(Story story); 
        public Task AddParagraph(Paragraph paragraph);
        public Task<Choice> GetChoiceDetailsById(int idChoice);
    }
}
