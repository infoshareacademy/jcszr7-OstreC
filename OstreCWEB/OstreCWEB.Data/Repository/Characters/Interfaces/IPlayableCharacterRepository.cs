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
        public Task<PlayableCharacter> GetById(int id);
        public Task<List<PlayableCharacter>> GetAll(int id);
        public Task Update(PlayableCharacter playableCharacter);
        public Task Create(PlayableCharacter playableCharacter);
        public Task Delete(PlayableCharacter playableCharacter);
    }
}