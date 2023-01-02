using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using OstreCWEB.Data.Repository.Characters.Enums;

namespace OstreCWEB.Data.Repository.Characters.CoreClasses
{
    //Playable races will need to have all the "skills" listed for the moment of creation. 
    //TODO: Replace this by "Race builder" once Entity Framework is added. 
    public class PlayableRace
    {
        [Key]
        public int PlayableRaceId { get; set; }

        public string RaceName { get; set; }
        [NotMapped]
        public List<Skill> DefaultSkillsForClass { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int AmountOfSkillsToChoose { get; set; }


        //EF relation
        public List<PlayableCharacter> PlayableCharacter { get; set; }   
    }
}
