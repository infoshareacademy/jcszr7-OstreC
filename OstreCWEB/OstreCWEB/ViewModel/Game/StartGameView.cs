using OstreCWEB.ViewModel.Characters;
using OstreCWEB.ViewModel.Identity;
using OstreCWEB.ViewModel.StoryBuilder;
using System.ComponentModel;

namespace OstreCWEB.ViewModel.Game
{
    public class StartGameView
    {
        //This class will be merged with story reader next week. 
        public UserView User { get; set; }
        [DisplayName("Other characters")]

        public List<PlayableCharacterRow> OtherUsersCharacters { get; set; }
        [DisplayName("Other Stories")]
        public List<StoriesView> OtherUsersStories { get; set; }
        [DisplayName("Saved Game")]
        public UserParagraphView UserParagraph { get; set; }
        [DisplayName("Chosen Character")]
        public PlayableCharacterView ActiveCharacter { get; set; }
        [DisplayName("Chosen Story")]
        public StoriesView ActiveStory { get; set; }
        public StartGameView()
        {
            User = new UserView();
            OtherUsersCharacters = new List<PlayableCharacterRow>();
            OtherUsersStories = new List<StoriesView>();
        }
    }
}
