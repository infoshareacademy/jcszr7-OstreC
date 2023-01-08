using OstreCWEB.Data.Repository.Characters.CoreClasses;

namespace OstreCWEB.ViewModel.PlayableCharacter
{
    public class PlayableCharacterAttrView
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int TotalAttributePoints { get; set; }
        public int AvailableAttributePoints { get; set; }

    }
}
