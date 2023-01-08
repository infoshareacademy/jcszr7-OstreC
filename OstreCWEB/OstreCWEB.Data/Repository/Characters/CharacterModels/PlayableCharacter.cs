using OstreCWEB.Data.Repository.Characters.Enums;
using OstreCWEB.Data.Repository.Identity;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.Data.Repository.Characters.CoreClasses
{
    public class PlayableCharacter : Character
    {  
        public PlayableCharacter()
        {

        }
        //CharacterId used to "link" the character to a user in db later on. 
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]

        public PlayableRace Race { get; set; }
        public int RaceId { get; set; }
        public PlayableCharacterClass CharacterClass { get; set; }
        public int PlayableClassId { get; set; }
        public int TotalAttributePoints { get; set; }
        public int AvailableAttributePoints { get; set; }


    }
}
