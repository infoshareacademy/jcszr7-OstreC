﻿using System.ComponentModel;

namespace OstreCWEB.ViewModel.Characters
{
    public class PlayableCharacterRow
    { 
        public int CharacterId { get; set; }

        [DisplayName("Character Name")]
        public string CharacterName { get; set; }
        [DisplayName("Character Race")]
        public PlayableRaceView Race { get; set; }
        [DisplayName("Character Class")]
        public PlayableClassView CharacterClass { get; set; } 
    }
}