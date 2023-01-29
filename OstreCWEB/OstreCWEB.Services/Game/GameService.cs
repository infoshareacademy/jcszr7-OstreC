﻿using OstreCWEB.Data.DataBase.ManyToMany;
using OstreCWEB.Data.Factory;
using OstreCWEB.Data.Repository.Characters.CharacterModels;
using OstreCWEB.Data.Repository.Characters.Interfaces;
using OstreCWEB.Data.Repository.Identity;
using OstreCWEB.Data.Repository.ManyToMany;
using OstreCWEB.Data.Repository.StoryModels;
using OstreCWEB.Data.Repository.StoryModels.Enums;
using OstreCWEB.Data.Repository.StoryModels.Properties;

namespace OstreCWEB.Services.Game
{
    internal class GameService : IGameService
    {
        private readonly IUserParagraphRepository _userParagraphRepository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IStoryRepository _storyRepository;
        private readonly IPlayableCharacterRepository _playableCharacterRepository;
        private readonly ICharacterFactory _characterFactory;

        public GameService(
            IUserParagraphRepository userParagraphRepository,
            IIdentityRepository identityRepository,
            IStoryRepository storyRepository,
            IPlayableCharacterRepository playableCharacter,
            ICharacterFactory characterFactory
            )
        {
            _userParagraphRepository = userParagraphRepository;
            _identityRepository = identityRepository;
            _storyRepository = storyRepository;
            _playableCharacterRepository = playableCharacter;
            _characterFactory = characterFactory;
        }
        public async Task<UserParagraph> CreateNewGameInstanceAsync(string userId, int characterTemplateId, int storyId)
        {
            var user = await _identityRepository.GetUser(userId);
            if (user.UserParagraphs.Count >= 5) { throw new Exception(); } 
            var newGameInstance = new UserParagraph(); 

            var story = await  _storyRepository.GetStoryNoIncludesAsync(storyId); 

            newGameInstance.User = user;
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


        public  Task<List<Enemy>> GenerateEnemies(List<EnemyInParagraph> enemiesToGenerate)
        { 
            return _characterFactory.CreateEnemiesInstances(enemiesToGenerate); 
        }

        public async Task NextParagraphAsync(string userId, int choiceId)
        {
            var userParagraph = await _userParagraphRepository.GetActiveByUserIdAsync(userId);
            userParagraph.Paragraph = await _storyRepository.GetParagraphById(userParagraph.Paragraph.Choices[choiceId].NextParagraphId);
            await _userParagraphRepository.UpdateAsync(userParagraph);
        }
        public async Task NextParagraphAfterFightAsync(UserParagraph gameInstance,int choiceId)
        {
            gameInstance.Paragraph = await _storyRepository.GetParagraphById(gameInstance.Paragraph.Choices[choiceId].NextParagraphId);
            await _userParagraphRepository.UpdateAsync(gameInstance);
        }
        public async Task DeleteGameInstanceAsync(int userParagrahId)
        {
            var userParagraph = await _userParagraphRepository.GetByUserParagraphIdAsync(userParagrahId);
            await _userParagraphRepository.Delete(userParagraph);
        }

        public async Task SetActiveGameInstanceAsync(int userParagraphId, string userId)
        {
            var user = await _identityRepository.GetUser(userId);
            user.UserParagraphs.ForEach(s =>
            {
                if (s.UserParagraphId == userParagraphId) { s.ActiveGame = true; }
                else { s.ActiveGame = false; }
            });
            _identityRepository.Update(user);
        }


        public async Task HealCharacterAsync(string userId)
        {
            var userParagraph = await _userParagraphRepository.GetActiveByUserIdAsync(userId);
            userParagraph.ActiveCharacter.CurrentHealthPoints = userParagraph.ActiveCharacter.MaxHealthPoints;
            await _userParagraphRepository.UpdateAsync(userParagraph);
        }

        public async Task<int> TestThrowAsync(string userId, int rollValue)
        {
            var userParagraph = await _userParagraphRepository.GetActiveByUserIdAsync(userId);

            int testDifficulty = GetTestDifficulty(userParagraph.Paragraph.TestProp.TestDifficulty);
            int modifire = GetModifire();
            int result = rollValue + modifire;

            return result < testDifficulty ? 0 : 1; // 0 - Failure, 1 - Success
        }

        public int ThrowDice(int maxValue)
        {
            Random rnd = new Random();
            return rnd.Next(1, maxValue+1);
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
        private int GetModifire()
        {
            return 0;
        }
        public async Task UnequipItemAsync(int itemRelationId, string userId)
        {
           var gameInstance = await _userParagraphRepository.GetActiveByUserIdAsync(userId);
            gameInstance.ActiveCharacter.LinkedItems.SingleOrDefault(x => x.Id == itemRelationId).IsEquipped = false;
            await _userParagraphRepository.SaveChangesAsync();
        } 
        public async Task EquipItemAsync(int itemRelationId, string userId)
        {
            var gameInstance = await _userParagraphRepository.GetActiveByUserIdAsync(userId);
            //Add spaghetti code to find out if given item can be equipped! 
            gameInstance.ActiveCharacter.LinkedItems.SingleOrDefault(x => x.Id == itemRelationId).IsEquipped = true;
            await _userParagraphRepository.SaveChangesAsync();
        } 
    }
}
