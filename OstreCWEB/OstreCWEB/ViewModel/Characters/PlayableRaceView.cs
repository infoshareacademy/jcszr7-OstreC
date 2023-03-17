using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.ViewModel.Characters
{
    public class PlayableRaceView
    {
        [DisplayName(" PlayableCharacterCreateView")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Playable Race")]
        [RegularExpression(@"^(?:.*[a-z]){1,20}$", ErrorMessage = "Name can't contain numbers, must be provided and can't be longer than 20 characters.")]
        public string RaceName { get; set; }
        [Required]
        [DisplayName("Playable Race Description")]
        public string RaceDescription { get; set; }
        [Required]
        [DisplayName("Intelligence Bonus")]
        [Range(0, 5)]
        public int IntelligenceBonus { get; set; }
        [Required]
        [DisplayName("Strenght Bonus")]
        [Range(0, 5)]
        public int StrengthBonus { get; set; }
        [Required]
        [DisplayName("Wisdom Bonus")]
        [Range(0, 5)]
        public int WisdomBonus { get; set; }
        [Required]
        [DisplayName("Dexterity Bonus")] 
        [Range(0, 5)]
        public int DexterityBonus { get; set; }
        [Required]
        [DisplayName("Constitution Bonus")] 
        [Range(0, 5)]
        public int ConstitutionBonus { get; set; }
        [Required]
        [DisplayName("Charisma Bonus")] 
        [Range(0, 5)]
        public int CharismaBonus { get; set; }
    }
}
