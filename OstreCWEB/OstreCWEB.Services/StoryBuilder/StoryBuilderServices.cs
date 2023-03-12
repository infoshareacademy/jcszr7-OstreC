using AutoMapper;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.DomainModels.StoryModels.Enums;
using OstreCWEB.DomainModels.StoryModels.Properties;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.Repository.Repository.StoryRepo;
using OstreCWEB.Services.StoryBuilder.Models;
using OstreCWEB.Services.StoryBuilder.ModelsDto;

namespace OstreCWEB.Services.StoryBuilder
{
    internal class StoryBuilderServices : IStoryBuilderServices
    {
        private readonly IStoryRepository<Story> _storyRepository;
        private readonly IEnemyRepository<Enemy> _enemyRepository;
        private readonly IItemRepository<Item> _itemRepository;
        private readonly IMapper _mapper;

        public StoryBuilderServices(
            IStoryRepository<Story> storyRepository,
            IEnemyRepository<Enemy> enemyRepository,
            IItemRepository<Item> itemRepository,
            IMapper mapper)
        {
            _storyRepository = storyRepository;
            _enemyRepository = enemyRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public bool Exists(int id)
        {
            return _storyRepository.Exists(id);
        }

        public async Task<List<Story>> GetAllStories()
        {
            return await _storyRepository.GetAllStories();
        }

        public async Task<List<StoryView>> GetStoriesByUserId(int userId)
        {
            var storyByUser = await _storyRepository.GetStoriesByUserId(userId);
            var result = _mapper.Map<List<StoryView>>(storyByUser);

            return result;
        }

        public async Task<StoryView> GetStoryByIdAsync(int idStory)
        {
            var story = await _storyRepository.GetStoryByIdAsync(idStory);
            var result = _mapper.Map<StoryView>(story);

            return result;
        }

        public Story GetStoryById(int idStory)
        {
            return _storyRepository.GetStoryById(idStory);
        }

        public async Task<StoryParagraphsView> GetStoryWithParagraphsById(int idStory)
        {
            var paragraphs = await _storyRepository.GetStoryWithParagraphsById(idStory);
            var result = _mapper.Map<StoryParagraphsView>(paragraphs);

            return result;
        }

        //Story

        public async Task AddStory(StoryView newStory, int userId)
        {
            var story = _mapper.Map<Story>(newStory);

            story.UserId = userId;
            await _storyRepository.AddStory(story);
        }

        public async Task UpdateStory(StoryView uStory, int userId)
        {
            var story = await _storyRepository.GetStoryByIdAsync(uStory.Id);

            if (story.UserId == userId)
            {
                story.Name = uStory.Name;
                story.Description = uStory.Description;

                await _storyRepository.UpdateStory(story);
            }
            else
            {
                throw new Exception("This is not your Story");
            }
        }

        public async Task DeleteStory(int idStory, int userId)
        {
            var story = await _storyRepository.GetStoryByIdAsync(idStory);

            if (story.UserId == userId)
            {
                await _storyRepository.DeleteStory(story);
            }
            else
            {
                throw new Exception("This is not your Story");
            }
        }

        //Paragraph
        public async Task<Paragraph> GetParagraphById(int idParagraphId)
        {
            return await _storyRepository.GetParagraphById(idParagraphId);
        }

        public async Task AddParagraph(Paragraph paragraph, int userId)
        {
            var userStories = await _storyRepository.GetStoriesByUserId(userId);

            if (userStories.Any(s => s.Id == paragraph.StoryId))
            {
                if (paragraph.ParagraphType == ParagraphType.DescOfStage)
                {
                    await _storyRepository.AddParagraph(paragraph);
                }
                else if (paragraph.ParagraphType == ParagraphType.Test || paragraph.ParagraphType == ParagraphType.Fight)
                {
                    var failureParagraph = new Paragraph
                    {
                        StageDescription = "Failure - " + paragraph.StageDescription,
                        ParagraphType = ParagraphType.DescOfStage,
                        RestoreRest = false,
                        StoryId = paragraph.StoryId
                    };

                    var successParagraph = new Paragraph
                    {
                        StageDescription = "Success - " + paragraph.StageDescription,
                        ParagraphType = ParagraphType.DescOfStage,
                        RestoreRest = false,
                        StoryId = paragraph.StoryId
                    };

                    await _storyRepository.AddParagraph(successParagraph);
                    await _storyRepository.AddParagraph(failureParagraph);

                    paragraph.Choices = new List<Choice>
                    {
                        new Choice
                        {
                            ChoiceText = "Failure - " + paragraph.StageDescription,
                            NextParagraphId = failureParagraph.Id,
                        },
                        new Choice
                        {
                            ChoiceText = "Success - " + paragraph.StageDescription,
                            NextParagraphId = successParagraph.Id,
                        },
                    };
                    await _storyRepository.AddParagraph(paragraph);
                }
                else
                {
                    throw new NotImplementedException();
                }

                var stories = await _storyRepository.GetAllStories();
                var story = stories.FirstOrDefault(x => x.Id == paragraph.StoryId);
                if (story.GetAmountOfParagraphs() == 1)
                {
                    story.FirstParagraphId = story.Paragraphs[0].Id;
                    await _storyRepository.UpdateStory(story);
                }
            }
            else
            {
                throw new Exception("This is not your Story");
            }
        }

        public async Task DeleteParagraph(int idParagraph, int userId)
        {
            var paragraph = await _storyRepository.GetParagraphById(idParagraph);
            var story = await _storyRepository.GetStoryByIdAsync(paragraph.StoryId);

            if (story.UserId == userId)
            {
                await _storyRepository.DeleteParagraph(story.Paragraphs.FirstOrDefault(p => p.Id == idParagraph));
            }
            else
            {
                throw new Exception("This is not your Story");
            }
        }

        public async Task<ParagraphDetails> GetParagraphDetailsById(int idParagraph, int idStory)
        {
            var story = await _storyRepository.GetStoryByIdAsync(idStory);

            var paragraphDetails = new ParagraphDetails
            {
                StoryId = story.Id,
                NameOfStory = story.Name,
                DescriptionOfStory = story.Description,
                AmountOfParagraphs = story.GetAmountOfParagraphs(),
                CurrentParagraph = story.Paragraphs.FirstOrDefault(p => p.Id == idParagraph),
                NextParagraphs = new List<ParagraphWithCoice>(),
                PreviousParagraphs = new List<ParagraphWithCoice>()
            };

            if (paragraphDetails.CurrentParagraph.Choices.Count() != 0)
            {
                foreach (var item in paragraphDetails.CurrentParagraph.Choices)
                {
                    ParagraphWithCoice nextParagraphView;
                    if (item.NextParagraphId != -1)
                    {
                        var nextParagraph = story.Paragraphs.FirstOrDefault(p => p.Id == item.NextParagraphId);
                        nextParagraphView = new ParagraphWithCoice
                        {
                            Id = nextParagraph.Id,
                            ParagraphType = nextParagraph.ParagraphType,
                            StageDescription = nextParagraph.StageDescription,
                            AmountOfChoices = nextParagraph.GetAmountOfChoices(),
                            ChoiceId = item.Id,
                            DescriptionOfChoice = item.ChoiceText
                        };
                    }
                    else
                    {
                        nextParagraphView = new ParagraphWithCoice
                        {
                            Id = -1,
                            ParagraphType = default,
                            StageDescription = "The paragraph doesn't exist",
                            AmountOfChoices = 0,
                            ChoiceId = item.Id,
                            DescriptionOfChoice = item.ChoiceText
                        };
                    }
                    paragraphDetails.NextParagraphs.Add(nextParagraphView);
                }
            }

            var previousParagraphs = story.Paragraphs.Where(s => s.Choices.Any(c => c.NextParagraphId == idParagraph)).ToList();

            if (previousParagraphs.Count > 0)
            {
                foreach (var item in previousParagraphs)
                {
                    var choice = item.Choices.FirstOrDefault(item => item.NextParagraphId == idParagraph);
                    ParagraphWithCoice previousParagraph = new ParagraphWithCoice
                    {
                        Id = item.Id,
                        ParagraphType = item.ParagraphType,
                        StageDescription = item.StageDescription,
                        AmountOfChoices = item.GetAmountOfChoices(),
                        ChoiceId = choice.Id,
                        DescriptionOfChoice = choice.ChoiceText
                    };

                    paragraphDetails.PreviousParagraphs.Add(previousParagraph);
                }
            }

            paragraphDetails.Delete = !paragraphDetails.PreviousParagraphs.Any
                (x => x.ParagraphType == ParagraphType.Fight || x.ParagraphType == ParagraphType.Test);

            if (paragraphDetails.CurrentParagraph.ParagraphType == ParagraphType.Fight || paragraphDetails.CurrentParagraph.ParagraphType == ParagraphType.Test)
            {
                paragraphDetails.CreatChoice = false;
            }
            else
            {
                paragraphDetails.CreatChoice = true;
            }

            return paragraphDetails;
        }

        public async Task<EditParagraph> GetEditParagraphById(int paragraphId)
        {
            var paragraph = await _storyRepository.GetParagraphToEditById(paragraphId);

            var EditParagraph = new EditParagraph
            {
                Id = paragraph.Id,
                ParagraphType = paragraph.ParagraphType,
                StageDescription = paragraph.StageDescription,
                StoryId = paragraph.StoryId,
            };

            if (paragraph.ParagraphType == ParagraphType.Fight)
            {
                EditParagraph.ParagraphEnemies = new List<EnemyInParagraphService>();
                EditParagraph.FightPropId = paragraph.FightProp.Id;

                if (paragraph.FightProp.ParagraphEnemies.Count() != 0)
                {
                    foreach (var item in paragraph.FightProp.ParagraphEnemies)
                    {
                        EditParagraph.ParagraphEnemies.Add(
                            new EnemyInParagraphService
                            {
                                Id = item.Id,
                                AmountOfEnemy = item.AmountOfEnemy,
                                EnemyName = item.Enemy.CharacterName,
                                ParagraphId = paragraph.Id,
                                FightPropId = item.FightPropId,
                                EnemyId = item.EnemyId
                            });
                    }
                }
            }

            if (paragraph.ParagraphType == ParagraphType.Test)
            {
                EditParagraph.AbilityScores = paragraph.TestProp.AbilityScores;
                EditParagraph.TestDifficulty = paragraph.TestProp.TestDifficulty;
            }

            return EditParagraph;
        }

        public async Task UpdateParagraph(EditParagraph editParagraph, int userId)
        {
            var userStories = await _storyRepository.GetStoriesByUserId(userId);

            if (userStories.Any(s => s.Id == editParagraph.StoryId))
            {
                var paragraph = await _storyRepository.GetParagraphToEditById(editParagraph.Id);

                paragraph.StageDescription = editParagraph.StageDescription;
                paragraph.RestoreRest = editParagraph.RestoreRest;

                if (editParagraph.ParagraphType == ParagraphType.Test)
                {
                    paragraph.TestProp.TestDifficulty = editParagraph.TestDifficulty;
                    paragraph.TestProp.AbilityScores = editParagraph.AbilityScores;
                }

                await _storyRepository.UpdateParagraph(paragraph);
            }
            else
            {
                throw new Exception("This is not your Story");
            }
        }

        public async Task AddEnemyToParagraph(EnemyInParagraphService enemyInParagraphService)
        {
            var enemyInParagraph = new EnemyInParagraph
            {
                EnemyId = enemyInParagraphService.EnemyId,
                FightPropId = enemyInParagraphService.FightPropId,
                AmountOfEnemy = enemyInParagraphService.AmountOfEnemy
            };

            await _storyRepository.AddEnemyToParagraph(enemyInParagraph);
        }

        public async Task DeleteEnemyInParagraph(int enemyInParagraphId)
        {
            await _storyRepository.DeleteEnemyInParagraph(enemyInParagraphId);
        }

        public async Task<List<Enemy>> GetAllEnemies()
        {
            return await _enemyRepository.GetAllTemplatesAsync();
        }

        public async Task<List<Item>> GetAllItems()
        {
            return _itemRepository.GetAll();
        }

        public async Task<ChoiceDetails> GetChoiceDetailsById(int idChoice)
        {
            var choice = await _storyRepository.GetChoiceDetailsById(idChoice);

            var choiceDetails = new ChoiceDetails
            {
                StoryId = choice.Paragraph.Story.Id,
                DescriptionOfStory = choice.Paragraph.Story.Description,
                NameOfStory = choice.Paragraph.Story.Name,
                AmountOfParagraphs = choice.Paragraph.Story.GetAmountOfParagraphs(),
                PreviousParagraph = choice.Paragraph,
                CurrentChoice = choice,
                NextParagraph = choice.Paragraph.Story.Paragraphs.FirstOrDefault(p => p.Id == choice.NextParagraphId)
            };

            if (choiceDetails.PreviousParagraph.ParagraphType == ParagraphType.Fight || choiceDetails.PreviousParagraph.ParagraphType == ParagraphType.Test)
            {
                choiceDetails.Delete = false;
            }
            else
            {
                choiceDetails.Delete = true;
            }

            return choiceDetails;
        }

        public async Task<ChoiceCreator> GetChoiceCreator(int firstParagraphId, int secondParagraphId)
        {
            var firstParagraph = await _storyRepository.GetParagraphById(firstParagraphId);
            var secondParagraph = await _storyRepository.GetParagraphById(secondParagraphId);

            var choiceCreator = new ChoiceCreator
            {
                ChangePlaces = false,

                ParagraphId = firstParagraph.Id,
                PreviousParagraph = firstParagraph,

                NextParagraphId = secondParagraph.Id,
                NextParagraph = secondParagraph,

                StoryId = firstParagraph.StoryId
            };
            return choiceCreator;
        }

        public async Task<ChoiceCreator> GetChoiceCreatorById(int choiceId)
        {
            var choice = await _storyRepository.GetChoiceDetailsById(choiceId);
            var nextParagraph = await _storyRepository.GetParagraphById(choice.NextParagraphId);

            var choiceCreator = new ChoiceCreator
            {
                Id = choiceId,

                ChangePlaces = false,
                ChoiceText = choice.ChoiceText,

                ParagraphId = choice.ParagraphId,
                PreviousParagraph = choice.Paragraph,

                NextParagraphId = choice.NextParagraphId,
                NextParagraph = nextParagraph,

                StoryId = choice.Paragraph.StoryId
            };

            return choiceCreator;
        }

        public async Task<ChoiceCreator> GetChoiceCreatorById(int choiceId, int secondParagraphId)
        {
            var choice = await _storyRepository.GetChoiceDetailsById(choiceId);
            var nextParagraph = await _storyRepository.GetParagraphById(secondParagraphId);

            var choiceCreator = new ChoiceCreator
            {
                Id = choiceId,

                ChangePlaces = false,
                ChoiceText = choice.ChoiceText,

                ParagraphId = choice.ParagraphId,
                PreviousParagraph = choice.Paragraph,

                NextParagraphId = secondParagraphId,
                NextParagraph = nextParagraph,

                StoryId = choice.Paragraph.StoryId
            };

            return choiceCreator;
        }

        public async Task UpdateChoice(ChoiceCreator choiceCreator)
        {
            var choice = await _storyRepository.GetChoiceDetailsById(choiceCreator.Id);

            choice.ChoiceText = choiceCreator.ChoiceText;
            choice.NextParagraphId = choiceCreator.NextParagraphId;

            await _storyRepository.UpdateChoice(choice);
        }

        public async Task AddChoice(ChoiceCreator choiceCreator)
        {
            var choice = new Choice();

            if (!choiceCreator.ChangePlaces)
            {
                choice = new Choice
                {
                    ChoiceText = choiceCreator.ChoiceText,
                    NextParagraphId = choiceCreator.NextParagraphId,
                    ParagraphId = choiceCreator.ParagraphId
                };
            }
            else
            {
                choice = new Choice
                {
                    ChoiceText = choiceCreator.ChoiceText,
                    NextParagraphId = choiceCreator.ParagraphId,
                    ParagraphId = choiceCreator.NextParagraphId
                };
            }

            await _storyRepository.AddChoice(choice);
        }

        public async Task DeleteChoice(int choiceId)
        {
            await _storyRepository.DeleteChoice(choiceId);
        }
    }
}