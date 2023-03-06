using OstreCWeb.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.DomainModels.CharacterModels
{
    public class PlayableRace : IEntityBase
    {
        //Ef Config// 
        [Key]
        public int Id { get; set; }
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
