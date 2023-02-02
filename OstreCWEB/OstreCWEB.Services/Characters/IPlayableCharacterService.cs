﻿using OstreCWEB.Data.Repository.Characters.CharacterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OstreCWEB.Data.Repository.Characters.CharacterModels;
using OstreCWEB.Data.Repository.Characters.Enums;
namespace OstreCWEB.Services.Characters
{
    public interface IPlayableCharacterService
    {
        public Task<PlayableCharacter> GetById(int id);
        public Task Create(PlayableCharacter playableCharacter);
        public Task<List<PlayableCharacter>> GetAll(); 
        public Task<List<PlayableCharacter>> GetAllTemplates(string id);
        public Task Add(Character charater);
        public Task Update(Character charater);
        public Task Remove(Character charater);
        #region
        public void CreateNew(PlayableCharacter model);
        public List<PlayableRace> GetAllRaces();
        public List<PlayableClass> GetAllClasses();
        public void RollAttributes(PlayableCharacter model);
        public Task<string> GetClassNameById(int id);
        public List<string> GetAllNames();
        public string GetRaceDescription(int id);
        public string GetClassDescription(int id);
        #endregion
        public int RollDice(int maxValue = 7);
    }
}
