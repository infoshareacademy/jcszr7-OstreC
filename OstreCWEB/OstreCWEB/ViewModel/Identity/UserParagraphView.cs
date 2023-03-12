using OstreCWEB.ViewModel.Characters;
using OstreCWEB.ViewModel.Game;
using OstreCWEB.Services.StoryBuilder.ModelsDto;

namespace OstreCWEB.ViewModel.Identity
{
    public class UserParagraphView
    {
        public int Id { get; set; }
        public UserView User { get; set; }
        public GameParagraphView Paragraph { get; set; }
        public PlayableCharacterView ActiveCharacter { get; set; }
        public StoriesView Story { get; set; }
        public bool ActiveGame { get; set; }
    }
}