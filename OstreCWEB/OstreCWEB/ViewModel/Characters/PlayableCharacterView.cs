﻿using OstreCWEB.Data.DataBase.ManyToMany;
using OstreCWEB.Data.Repository.Characters.CharacterModels;
using OstreCWEB.Data.Repository.Characters.MetaTags;

namespace OstreCWEB.ViewModel.Characters
{
    public class PlayableCharacterView
    { 
        public int CharacterId { get; set; } 
        public string CharacterName { get; set; } 
        public PlayableRaceView Race { get; set; } 
        public PlayableClassView CharacterClass { get; set; }
        public int MaxHealthPoints { get; set; }
        public int Level { get; set; }
        public int Strenght { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
    }
}