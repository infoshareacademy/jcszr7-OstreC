using System.ComponentModel;

namespace OstreCWEB.ViewModel.Characters
{
    public class PlayableClassView
    {
        [DisplayName(" Id")]
        public int PlayableClassId { get; set; }
        [DisplayName("Class Name")]
        public string ClassName { get; set; }
        [DisplayName("Intelligence Bonus")]
        public int BaseHP { get; set; }
        public int IntelligenceBonus { get; set; }
        [DisplayName("Strenght Bonus")]
        public int StrengthBonus { get; set; }
        [DisplayName("Wisdom Bonus")]
        public int WisdomBonus { get; set; }
        [DisplayName("Dexterity Bonus")]
        public int DexterityBonus { get; set; }
        [DisplayName("Constitution Bonus")]
        public int ConstitutionBonus { get; set; }
        [DisplayName("Charisma Bonus")]
        public int CharismaBonus { get; set; }
    }
}
