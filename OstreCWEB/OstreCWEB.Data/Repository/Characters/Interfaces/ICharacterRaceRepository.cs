﻿using OstreCWEB.Data.Repository.Characters.CharacterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Data.Repository.Characters.Interfaces
{
    public interface ICharacterRaceRepository
    {
        public PlayableRace GetById(int id);
        public Task<PlayableRace> GetByIdAsync(int id);
        public Task<List<PlayableRace>> GetAllAsync();
        public Task UpdateAsync(PlayableRace item);
        public Task CreateAsync(PlayableRace item);
        public Task DeleteAsync(int id);
    }
}
