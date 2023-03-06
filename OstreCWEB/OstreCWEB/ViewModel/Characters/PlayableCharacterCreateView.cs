using System.ComponentModel.DataAnnotations.Schema;

namespace OstreCWEB.ViewModel.Characters
{
    public class PlayableCharacterCreateView
    {
        public int Id { get; set; }
        public PlayableRaceView Race { get; set; }
        public int RaceId { get; set; }
        public PlayableClassView CharacterClass { get; set; }
        public int PlayableClassId { get; set; }

        public string CharacterName { get; set; }

        public int MaxHealthPoints { get; set; }
        public int CurrentHealthPoints { get; set; }

        public int Strenght { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        [NotMapped]
        public Dictionary<int, string> CharacterClasses { get; set; }
        public Dictionary<int, string> CharacterRaces { get; set; }
    }
}
