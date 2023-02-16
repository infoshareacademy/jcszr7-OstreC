using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.DomainModels.StoryModels.Properties;

#nullable disable

namespace OstreCWEB.Repository.Repository.StoryModels
{
    internal class StoryRepository : IStoryRepository
    {
        private readonly OstreCWebContext _ostreCWebContext;

        public StoryRepository(OstreCWebContext ostreCWebContext)
        {
            _ostreCWebContext = ostreCWebContext;
        }

        public bool Exists(int storyId)
        {
            return _ostreCWebContext.Stories.Any(story => story.Id == storyId);
        }
        public async Task<IReadOnlyCollection<Story>> GetAllStories()
        {
            return _ostreCWebContext.Stories
                .Include(s => s.Paragraphs)
                .ToList();
        }

        public async Task<IReadOnlyCollection<Story>> GetStoriesByUserId(string userId)
        {
            return _ostreCWebContext.Stories
                .Where(s => s.UserId == userId)
                .Include(s => s.Paragraphs)
                .ToList();
        }

        public async Task<Story> GetStoryByIdAsync(int idStory)
        {
            return _ostreCWebContext.Stories
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.Choices)
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.FightProp)
                        .ThenInclude(f => f.ParagraphEnemies)
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.DialogProp)
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.TestProp)
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.ShopkeeperProp)
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.ParagraphItems)
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.UserParagraphs)
                .SingleOrDefault(s => s.Id == idStory);
        }
        public Story GetStoryById(int idStory)
        {
            return _ostreCWebContext.Stories
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.Choices)
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.FightProp)
                        .ThenInclude(f => f.ParagraphEnemies)
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.DialogProp)
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.TestProp)
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.ShopkeeperProp)
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.ParagraphItems)
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.UserParagraphs)
                .SingleOrDefault(s => s.Id == idStory);
        }

        public async Task<Story> GetStoryWithParagraphsById(int idStory)
        {
            return _ostreCWebContext.Stories
                .Include(s => s.Paragraphs)
                    .ThenInclude(p => p.Choices)
                .SingleOrDefault(s => s.Id == idStory);
        }

        public async Task<Story> GetStoryNoIncludesAsync(int storyId)
        {
            return await _ostreCWebContext.Stories.SingleOrDefaultAsync(s => s.Id == storyId);
        }

        public async Task<Paragraph> GetParagraphById(int idParagraph)
        {
            return _ostreCWebContext.Paragraphs
                .Include(p => p.ParagraphItems)
                .Include(p => p.Choices)
                .SingleOrDefault(p => p.Id == idParagraph);
        }
        public async Task<Paragraph> GetCombatParagraphById(int idParagraph)
        {
            return await _ostreCWebContext.Paragraphs
                            .Include(p => p.FightProp)
                            .SingleOrDefaultAsync();
        }
        public async Task AddStory(Story story)
        {
            _ostreCWebContext.Stories.Add(story);
            await _ostreCWebContext.SaveChangesAsync();
        }

        public async Task UpdateStory(Story story)
        {
            _ostreCWebContext.Stories.Update(story);
            await _ostreCWebContext.SaveChangesAsync();
        }

        public async Task UpdateParagraph(Paragraph paragraph)
        {
            _ostreCWebContext.Paragraphs.Update(paragraph);
            await _ostreCWebContext.SaveChangesAsync();
        }

        public async Task DeleteStory(Story story)
        {
            _ostreCWebContext.Stories.Remove(story);
            await _ostreCWebContext.SaveChangesAsync();
        }

        public async Task AddParagraph(Paragraph paragraph)
        {
            _ostreCWebContext.Paragraphs.Add(paragraph);
            await _ostreCWebContext.SaveChangesAsync();
        }

        public async Task AddEnemyToParagraph(EnemyInParagraph enemyInParagraph)
        {
            _ostreCWebContext.EnemyInParagraphs.Add(enemyInParagraph);
            await _ostreCWebContext.SaveChangesAsync();
        }

        public async Task<EnemyInParagraph> GetEnemyInParagraphById(int id)
        {
            return _ostreCWebContext.EnemyInParagraphs
                .SingleOrDefault(ep => ep.Id == id);
        }

        public async Task DeleteEnemyInParagraph(int enemyInParagraphId)
        {
            _ostreCWebContext.EnemyInParagraphs.Remove(await GetEnemyInParagraphById(enemyInParagraphId));
            await _ostreCWebContext.SaveChangesAsync();
        }


        public async Task DeleteParagraph(Paragraph paragraph)
        {
            _ostreCWebContext.Paragraphs.Remove(paragraph);
            await _ostreCWebContext.SaveChangesAsync();
        }

        public async Task<Paragraph> GetParagraphToEditById(int paragraphId)
        {
            return await _ostreCWebContext.Paragraphs
                .Include(p => p.Choices)
                .Include(p => p.FightProp)
                    .ThenInclude(f => f.ParagraphEnemies)
                        .ThenInclude(pe => pe.Enemy)
                .Include(p => p.DialogProp)
                .Include(p => p.TestProp)
                .Include(p => p.ShopkeeperProp)
                .Include(p => p.ParagraphItems)
                .SingleOrDefaultAsync(s => s.Id == paragraphId);

        }

        public async Task<Choice> GetChoiceDetailsById(int idChoice)
        {
            return _ostreCWebContext.Choices
                .Include(c => c.Paragraph)
                    .ThenInclude(p => p.Story)
                        .ThenInclude(s => s.Paragraphs)
                .SingleOrDefault(c => c.Id == idChoice);
        }

        public async Task AddChoice(Choice choice)
        {
            _ostreCWebContext.Choices.Add(choice);
            await _ostreCWebContext.SaveChangesAsync();
        }

        public async Task DeleteChoice(int choiceId)
        {
            var choice = _ostreCWebContext.Choices.SingleOrDefault(c => c.Id == choiceId);

            _ostreCWebContext.Choices.Remove(choice);
            await _ostreCWebContext.SaveChangesAsync();
        }

        public async Task UpdateChoice(Choice choice)
        {
            _ostreCWebContext.Choices.Update(choice);
            await _ostreCWebContext.SaveChangesAsync();
        }
    }
}
