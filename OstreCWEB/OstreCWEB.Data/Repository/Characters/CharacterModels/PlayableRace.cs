﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OstreCWEB.Data.Repository.Characters.CharacterModels
{
    public class PlayableRace
    {
        //Ef Config// 
        [Key]
        public int PlayableRaceId { get; set; }
        public List<PlayableCharacter> PlayableCharacter { get; set; } 
        // 
        public string RaceName { get; set; } 
        public int IntelligenceBonus { get; set; }
        public int StrengthBonus { get; set; }
        public int WisdomBonus { get; set; }
        public int DexterityBonus { get; set; }
        public int ConstitutionBonus { get; set; }
        public int CharismaBonus { get; set; }

    }
}
