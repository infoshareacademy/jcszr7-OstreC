using System.ComponentModel;

namespace OstreCWEB.ViewModel.Characters
{
    public class PlayableCharacterRow
    {
        public int Id { get; set; }

        [DisplayName("Character Name")]
        public string CharacterName { get; set; }
        [DisplayName("Character Race")]
        public string RaceName { get; set; }
        [DisplayName("Character Class")]
        public string CharacterClassName { get; set; }
        public bool IsTemplate { get; set; }
    }
}