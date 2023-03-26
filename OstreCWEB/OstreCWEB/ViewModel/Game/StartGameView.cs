using OstreCWEB.ViewModel.Characters;
using OstreCWEB.ViewModel.Identity;
using OstreCWEB.ViewModel.StoryBuilder;
using System.ComponentModel;

namespace OstreCWEB.ViewModel.Game
{
    public class StartGameView
    {
        //This class will be merged with story reader next week.  
        [DisplayName("Other characters")]
        public List<PlayableCharacterRow> UserCharacters { get; set; }
        public List<StoriesView> UserStories { get; set; }
        public List<PlayableCharacterRow> OtherUsersCharacters { get; set; }
        [DisplayName("Other Stories")]
        public List<StoriesView> OtherUsersStories { get; set; }
        [DisplayName("Saved Game")]
        public List<UserParagraphView> UserParagraphs { get; set; }
        [DisplayName("Chosen Character")]
        public string ActiveCharacterName { get; set; }
        [DisplayName("Chosen Story")]
        public string ActiveStoryName { get; set; }
        public StartGameView()
        { 
            OtherUsersCharacters = new List<PlayableCharacterRow>();
            OtherUsersStories = new List<StoriesView>();
            UserCharacters = new List<PlayableCharacterRow>();
            UserStories = new List<StoriesView>();
        }
    }
}
