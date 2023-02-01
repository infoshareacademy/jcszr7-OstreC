﻿using Microsoft.AspNetCore.Authorization.Infrastructure;
using OstreCWEB.Data.Repository.Characters.CharacterModels;
using OstreCWEB.Data.Repository.Characters.Enums;
using OstreCWEB.Data.Repository.Characters.Interfaces;

namespace OstreCWEB.Services.Characters
{
    public class PlayableCharacterService : IPlayableCharacterService 
    {
        private readonly IPlayableCharacterRepository _playableCharacterRepository;
        private readonly ICharacterClassRepository _characterClassRepository;
        private readonly ICharacterRaceRepository _characterRaceRepository;

        public PlayableCharacterService(IPlayableCharacterRepository characterRepository, ICharacterClassRepository characterClassRepository, ICharacterRaceRepository characterRaceRepository)
        {
            _playableCharacterRepository = characterRepository;
            _characterClassRepository = characterClassRepository;
            _characterRaceRepository = characterRaceRepository;
        } 
        public Task Add(Character charater)
        {
            throw new NotImplementedException();
        }
        public Task Create(PlayableCharacter playableCharacter)
        {            
            playableCharacter.CharacterClass = _characterClassRepository.GetById(playableCharacter.PlayableClassId);
            playableCharacter.Race = _characterRaceRepository.GetById(playableCharacter.RaceId);
            playableCharacter.Strenght = playableCharacter.Strenght + playableCharacter.CharacterClass.StrengthBonus + playableCharacter.Race.StrengthBonus;
            playableCharacter.Dexterity = playableCharacter.Dexterity + playableCharacter.CharacterClass.DexterityBonus + playableCharacter.Race.DexterityBonus;
            playableCharacter.Constitution = playableCharacter.Constitution + playableCharacter.CharacterClass.ConstitutionBonus + playableCharacter.Race.ConstitutionBonus;
            playableCharacter.Intelligence = playableCharacter.Intelligence + playableCharacter.CharacterClass.IntelligenceBonus + playableCharacter.Race.IntelligenceBonus;
            playableCharacter.Wisdom = playableCharacter.Wisdom + playableCharacter.CharacterClass.WisdomBonus + playableCharacter.Race.WisdomBonus;
            playableCharacter.Charisma = playableCharacter.Charisma + playableCharacter.CharacterClass.CharismaBonus + playableCharacter.Race.CharismaBonus;
            playableCharacter.CurrentHealthPoints = playableCharacter.MaxHP();
            playableCharacter.MaxHealthPoints = playableCharacter.MaxHP();
            playableCharacter.IsTemplate = true;

            return _playableCharacterRepository.Create(playableCharacter);
        }
        public Task<List<PlayableCharacter>> GetAll()
        {
            return _playableCharacterRepository.GetAllTemplatesAsync();
        }
        /// <summary>
        /// Gets all playable characters except those belonging to a given  user
        /// </summary>
        /// <param name="userCharacters"></param>
        /// <returns></returns>
        public async Task<List<PlayableCharacter>> GetAllTemplates(string userId)
        {
            return await _playableCharacterRepository.GetAllTemplatesExceptAsync(userId);
        } 
        public async Task<PlayableCharacter> GetById(int id)
        {
            return await _playableCharacterRepository.GetByIdAsync(id);
        } 
        public Task Remove(Character charater)
        {
            throw new NotImplementedException();
        } 
        public Task Update(Character charater)
        {
            throw new NotImplementedException();
        }
        #region
        public void CreateNew(PlayableCharacter model)
        {
            _playableCharacterRepository.CreateNew(model);
        }
        public List<PlayableRace> GetAllRaces()
        {
            return _playableCharacterRepository.GetAllRaces();
        }
        public List<PlayableClass> GetAllClasses()
        {
            return _playableCharacterRepository.GetAllClasses();
        }
        public void RollAttributes(PlayableCharacter model)
        {
            _playableCharacterRepository.RollAttributes(model);
        }

        #endregion
        #region quickAutisticMethod
        public int RollDice(int maxValue = 7)
        {
            int[] rolls = new int[4];

            Random rng = new Random();
            for (int i = 0; i < rolls.Length; i++)
            {
                rolls[i] = rng.Next(1, maxValue);
            }
            Array.Sort(rolls);
            Array.Reverse(rolls);

            int sum = rolls.Take(3).Sum();
            return sum;
        }

        public async Task<string> GetClassNameById(int id)
        {
            return "error";
        }
        public List<string> GetAllNames()
        {
            return _playableCharacterRepository.GetAllNames();
        }
        #endregion
    }
}
