using OstreCWEB.ViewModel.Characters;
using OstreCWEB.ViewModel.Game;

namespace OstreCWEB.ViewModel.Identity
{
    public class UserParagraphView
    {
        public int Id { get; set; } 
        public GameParagraphView Paragraph { get; set; }
        public PlayableCharacterView ActiveCharacter { get; set; }
        public string RelatedStoryName { get; set; }
        public string RelatedCharacterName { get; set; }
        public string RelatedCharacterRace { get; set; }
        public string RelatedCharacterClass { get; set; } 
    }
}