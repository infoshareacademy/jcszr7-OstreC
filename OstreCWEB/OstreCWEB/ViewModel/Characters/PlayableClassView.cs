using System.ComponentModel;

namespace OstreCWEB.ViewModel.Characters
{
    public class PlayableClassView
    {
        [DisplayName(" PlayableCharacterCreateView")]
        public int Id { get; set; }
        [DisplayName("Class Name")]
        public string ClassName { get; set; }
        [DisplayName("Class Description")]
        public string ClassDescription { get; set; }
        [DisplayName("Base Health")]
        public int BaseHP { get; set; }
        [DisplayName("Intelligence Bonus")]
        public int IntelligenceBonus { get; set; }
        [DisplayName("Strength Bonus")]
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
