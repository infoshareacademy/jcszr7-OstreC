using OstreCWEB.ViewModel.Characters;
using OstreCWEB.ViewModel.Identity;
using OstreCWEB.Services.StoryService.ModelsDto;
using System.ComponentModel;

namespace OstreCWEB.ViewModel.Game
{
    public class StartGameView
    {
        //This class will be merged with story reader next week.  
        [DisplayName("Other characters")]
        public List<PlayableCharacterRow> UserCharacters { get; set; }
        public List<StoryView> UserStories { get; set; }
        public List<PlayableCharacterRow> OtherUsersCharacters { get; set; }

        [DisplayName("Other Stories")]
        public List<StoryView> OtherUsersStories { get; set; }

        [DisplayName("Saved Game")]
        public List<UserParagraphView> UserParagraphs { get; set; }
        [DisplayName("Chosen Character")]
        public string ActiveCharacterName { get; set; }
        [DisplayName("Chosen Story")]
        public string ActiveStoryName { get; set; }
        public StartGameView()
        { 
            OtherUsersCharacters = new List<PlayableCharacterRow>();
            OtherUsersStories = new List<StoryView>();
            UserCharacters = new List<PlayableCharacterRow>();
            UserStories = new List<StoryView>();
        }
    }
}