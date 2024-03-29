﻿using OstreCWEB.Data.Repository.Characters.CharacterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Data.Repository.Characters.Interfaces
{
    public interface IPlayableCharacterRepository
    {
        public Task<PlayableCharacter> GetByIdAsync(int id);
        public Task<List<PlayableCharacter>> GetAllTemplatesAsync(); 
        public Task<List<PlayableCharacter>> GetAllTemplatesExceptAsync(string id);
        public Task UpdateAsync(PlayableCharacter playableCharacter);
        public Task<PlayableCharacter> Create(PlayableCharacter playableCharacter);
        public Task DeleteAsync(PlayableCharacter playableCharacter);
        public Task<PlayableCharacter> GetByIdNoTrackingAsync(int characterTemplateId);
        public Task UpdateAlreadyTrackedAsync(PlayableCharacter playableCharacter);

        #region

        public void CreateNew(PlayableCharacter model);
        public List<PlayableRace> GetAllRaces();
        public List<PlayableClass> GetAllClasses();
        public void RollAttributes(PlayableCharacter model);
        public List<string> GetAllNames();
        public string GetRaceDescription(int id);
        public string GetClassDescription(int id);

        #endregion
        public bool Exists(int id);
    }
}
