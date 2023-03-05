using OstreCWEB.Repository.Factory;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.Repository.Repository.Identity;
using OstreCWEB.Repository.Repository.ManyToMany;
using OstreCWEB.Repository.Repository.StoryModels;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.DomainModels.StoryModels.Properties;
using OstreCWEB.DomainModels.StoryModels.Enums;

namespace OstreCWEB.Services.Game
{
    internal class GameService : IGameService
    {
        private readonly IUserParagraphRepository _userParagraphRepository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IStoryRepository _storyRepository;
        private readonly IPlayableCharacterRepository _playableCharacterRepository;
        private readonly ICharacterFactory _characterFactory;
        private readonly IItemCharacterRepository _itemCharacterRepository;

        public GameService(
            IUserParagraphRepository userParagraphRepository,
            IIdentityRepository identityRepository,
            IStoryRepository storyRepository,
            IPlayableCharacterRepository playableCharacter,
            ICharacterFactory characterFactory,
            IItemCharacterRepository itemCharacterRepository)
        {
            _userParagraphRepository = userParagraphRepository;
            _identityRepository = identityRepository;
            _storyRepository = storyRepository;
            _playableCharacterRepository = playableCharacter;
            _characterFactory = characterFactory;
            _itemCharacterRepository = itemCharacterRepository;
        }
        public async Task<UserParagraph> CreateNewGameInstanceAsync(int userId, int characterTemplateId, int storyId)
        {
            var user = await _identityRepository.GetUser(userId); 
            if (user.UserParagraphs.Count >= 5) { throw new Exception(); }
            var newGameInstance = new UserParagraph();

            var story = await _storyRepository.GetStoryNoIncludesAsync(storyId);

            //newGameInstance.User = user;
            newGameInstance.Paragraph = await _storyRepository.GetParagraphById(story.FirstParagraphId);

            var notTrackedCharacter = await _playableCharacterRepository.GetByIdNoTrackingAsync(characterTemplateId);
            var newCharacterInstance = _characterFactory.CreatePlayableCharacterInstance(notTrackedCharacter).Result;

            newGameInstance.ActiveCharacter = newCharacterInstance;
            newGameInstance.ActiveGame = true;

            user.UserParagraphs.ForEach(c => c.ActiveGame = false);
            user.UserParagraphs.Add(newGameInstance);
            await _identityRepository.Update(user);
            return newGameInstance;
        }



        public Task<List<Enemy>> GenerateEnemies(List<EnemyInParagraph> enemiesToGenerate)
        {
            return _characterFactory.CreateEnemiesInstances(enemiesToGenerate);
        }

        public async Task NextParagraphAsync(int userId, int choiceId)
        {
            var userParagraph = await _userParagraphRepository.GetActiveByUserIdAsync(userId);
            userParagraph.Paragraph = await _storyRepository.GetParagraphById(userParagraph.Paragraph.Choices[choiceId].NextParagraphId);

            if (userParagraph.Paragraph.ParagraphItems.Count > 0)
            {
                await AddItem(userParagraph.ActiveCharacter, userParagraph.Paragraph.ParagraphItems);
            }
            userParagraph.Rest = userParagraph.Paragraph.RestoreRest;

            await _userParagraphRepository.UpdateAsync(userParagraph);
        }
        public async Task NextParagraphAfterFightAsync(UserParagraph gameInstance, int choiceId)
        {
            gameInstance.Paragraph = await _storyRepository.GetParagraphById(gameInstance.Paragraph.Choices[choiceId].NextParagraphId);

            if (gameInstance.Paragraph.ParagraphItems.Count > 0)
            {
                await AddItem(gameInstance.ActiveCharacter, gameInstance.Paragraph.ParagraphItems);
            }
            gameInstance.Rest = gameInstance.Paragraph.RestoreRest;

            await _userParagraphRepository.UpdateAsync(gameInstance);
        }
        public async Task DeleteGameInstanceAsync(int userParagrahId)
        {
            var userParagraph = await _userParagraphRepository.GetByUserParagraphIdAsync(userParagrahId);
            await _userParagraphRepository.Delete(userParagraph);
        }

        public async Task SetActiveGameInstanceAsync(int userParagraphId, int userId)
        {
            var user = await _identityRepository.GetUser(userId);
            user.UserParagraphs.ForEach(s =>
            {
                if (s.UserParagraphId == userParagraphId) { s.ActiveGame = true; }
                else { s.ActiveGame = false; }
            });
            _identityRepository.Update(user);
        }


        public async Task HealCharacterAsync(int userId)
        {
            var userParagraph = await _userParagraphRepository.GetActiveByUserIdAsync(userId);
            userParagraph.ActiveCharacter.CurrentHealthPoints = userParagraph.ActiveCharacter.MaxHealthPoints;
            userParagraph.ActiveCharacter.LinkedAbilities.ForEach(x => x.UsesLeftBeforeRest = x.CharacterAction.UsesMaxBeforeRest);

            userParagraph.Rest = false;

            await _userParagraphRepository.UpdateAsync(userParagraph);
        }

        public async Task<int> TestThrowAsync(int userId, int rollValue, int modifire)
        {
            var userParagraph = await _userParagraphRepository.GetActiveByUserIdAsync(userId);

            int testDifficulty = GetTestDifficulty(userParagraph.Paragraph.TestProp.TestDifficulty);

            int result = rollValue + modifire;

            return result < testDifficulty ? 0 : 1; // 0 - Failure, 1 - Success
        }

        public async Task<int[]> ThrowDice(int maxValue, int userId)
        {
            var userParagraph = await _userParagraphRepository.GetActiveByUserIdAsync(userId);

            Random rnd = new Random();
            int roll = rnd.Next(1, maxValue + 1);

            int modifire;
            switch (userParagraph.Paragraph.TestProp.AbilityScores)
            {
                case AbilityScores.Wisdom:
                    modifire = GetModifire(userParagraph.ActiveCharacter.Wisdom);
                    break;
                case AbilityScores.Strength:
                    modifire = GetModifire(userParagraph.ActiveCharacter.Strenght);
                    break;
                case AbilityScores.Charisma:
                    modifire = GetModifire(userParagraph.ActiveCharacter.Charisma);
                    break;
                case AbilityScores.Constitution:
                    modifire = GetModifire(userParagraph.ActiveCharacter.Constitution);
                    break;
                case AbilityScores.Dexterity:
                    modifire = GetModifire(userParagraph.ActiveCharacter.Dexterity);
                    break;
                case AbilityScores.Intelligence:
                    modifire = GetModifire(userParagraph.ActiveCharacter.Intelligence);
                    break;
                default:
                    modifire = 0;
                    break;
            }

            int[] result = new int[2];
            result[0] = roll;
            result[1] = modifire;

            return result;
        }

        //Private
        private int GetTestDifficulty(TestDifficulty testDifficulty)
        {
            switch (testDifficulty)
            {
                case TestDifficulty.VeryEasy: return 5;
                case TestDifficulty.Easy: return 10;
                case TestDifficulty.Medium: return 15;
                case TestDifficulty.Hard: return 20;
                case TestDifficulty.VeryHard: return 25;
                case TestDifficulty.NearlyImpossible: return 30;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private int GetModifire(int value)
        {
            List<int> numbers = new List<int>() {
                -5,-4,-4,-3,-3,-2,-2,-1,-1, 0,
                 0, 1, 1, 2, 2, 3, 3, 4, 4, 5,
                 5, 6, 6, 7, 7, 8, 8, 9, 9, 10 };

            return numbers.First(x => x == numbers[value - 1]);
        }

        private async Task AddItem(PlayableCharacter activeCharacter, List<ParagraphItem> paragraphItems)
        {
            List<ItemCharacter> items = new List<ItemCharacter>();

            foreach (var item in paragraphItems)
            {
                for (int i = 0; i < item.AmountOfItems; i++)
                {

                    items.Add(
                        new ItemCharacter
                        {
                            CharacterId = activeCharacter.CharacterId,
                            ItemId = item.ItemId,
                            IsEquipped = false
                        });
                }
            }
            await _itemCharacterRepository.AddRange(items);
        }
        public async Task UnequipItemAsync(int itemRelationId, int userId)
        {
            var gameInstance = await _userParagraphRepository.GetActiveByUserIdAsync(userId);
            gameInstance.ActiveCharacter.LinkedItems.SingleOrDefault(x => x.Id == itemRelationId).IsEquipped = false;
            await _userParagraphRepository.SaveChangesAsync();
        }
        public async Task EquipItemAsync(int itemRelationId, int userId)
        {
            var gameInstance = await _userParagraphRepository.GetActiveByUserIdAsync(userId);
            var itemToEquip = gameInstance.ActiveCharacter.LinkedItems.SingleOrDefault(x => x.Id == itemRelationId);

            //Every item has specific condition, a two handed sword can't be used with a shield etc. 
            await DesequipAlreadyEquipped(gameInstance.ActiveCharacter, itemToEquip);
            if (itemToEquip.Item.ItemType != ItemType.SpecialItem)
            {
                itemToEquip.IsEquipped = true;
            }
            else
            {
                throw new Exception("Special Items can't be equipped!");
            }

            await _userParagraphRepository.SaveChangesAsync();
        }
        private async Task DesequipAlreadyEquipped(PlayableCharacter character, ItemCharacter itemToEquip)
        {
            switch (itemToEquip.Item.ItemType)
            {
                case ItemType.TwoHandedWeapon:
                    //Desequip shield and singleHandedWeapon
                    foreach (var item in character.LinkedItems)
                    {
                        if (item.IsEquipped && item.Item.ItemType == ItemType.Shield || item.Item.ItemType == ItemType.SingleHandedWeapon || item.Item.ItemType == ItemType.TwoHandedWeapon)
                        {
                            item.IsEquipped = false;
                        }
                    }
                    return;
                case ItemType.SingleHandedWeapon:
                    //desequip two handed weapon
                    foreach (var item in character.LinkedItems)
                    {
                        if (item.IsEquipped && item.Item.ItemType == ItemType.TwoHandedWeapon || item.Item.ItemType == ItemType.SingleHandedWeapon)
                        {
                            item.IsEquipped = false;
                        }
                    }
                    return;
                case ItemType.Shield:
                    //Desequip two handed weapon
                    foreach (var item in character.LinkedItems)
                    {
                        if (item.IsEquipped && item.Item.ItemType == ItemType.TwoHandedWeapon || item.Item.ItemType == ItemType.Shield)
                        {
                            item.IsEquipped = false;
                        }
                    }
                    return;
                case ItemType.SpecialItem:
                    return;
                default:
                    foreach (var item in character.LinkedItems)
                    {
                        if (item.IsEquipped && item.Item.ItemType == itemToEquip.Item.ItemType)
                        {
                            item.IsEquipped = false;
                        }
                    }
                    return;
            }

        }
    }
}
