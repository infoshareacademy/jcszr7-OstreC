using OstreCWEB.DomainModels.Identity;
using OstreCWEB.DomainModels.ManyToMany;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.DomainModels.CharacterModels
{
    public class PlayableCharacter : Character
    {
        //Ef config 
        public string UserId { get; set; }//Id of character owner
        public User User { get; set; }
        public UserParagraph? UserParagraph { get; set; }
        public int? UserParagraphId { get; set; }  
        public PlayableRace Race { get; set; }
        public int RaceId { get; set; } 
        public PlayableClass CharacterClass { get; set; }
        public int PlayableClassId { get; set; }
        //
        public PlayableCharacter() : base()
        {

        }

        public int MaxHP()
        {
            return this.CharacterClass.BaseHP + CalculateModifier(this.Constitution);
        }
        private int CalculateModifier(int value)
        {
            List<int> numbers = new List<int>() {
                   -5,-4,-4,-3,-3,-2,-2,-1,-1, 0,
                    0, 1, 1, 2, 2, 3, 3, 4, 4, 5,
                    5, 6, 6, 7, 7, 8, 8, 9, 9, 10 };

            return numbers.First(x => x == numbers[value - 1]);
        }
    }
}
