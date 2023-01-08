using OstreCWEB.Data.Repository.Characters.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Data.Repository.Characters.CoreClasses
{
    public class CharacterSkills
    {
        [Key]
        public int CharacterId { get; set; }
        public int AvailableSkillPoints { get; set; }

        //Dexterity - Acrobatics - Sleight of Hand - Stealth
        public int Athletics { get; set; }
        public int Acrobatics { get; set; }
        public int SleightOfHand { get; set; }
        public int Stealth { get; set; }

        //Intelligence - Arcana - History - Investigation - Nature - Religion
        public int Arcana { get; set; }
        public int History { get; set; }
        public int Investigation { get; set; }
        public int Nature { get; set; }
        public int Religion { get; set; }

        //Wisdom - Animal Handling - Insight - Medicine - Perception - Survival
        public int AnimalHandling { get; set; }
        public int Insight { get; set; }
        public int Medicine { get; set; }
        public int Perception { get; set; }
        public int Survival { get; set; }

        //Charisma - Deception - Intimidation - Performance - Persuasion
        public int Deception { get; set; }
        public int Intimidation { get; set; }
        public int Performance { get; set; }
        public int Persuasion { get; set; }

        //public string SkillName { get; set; }
        //public Statistics StatisticForTest { get; set; }
    }
}