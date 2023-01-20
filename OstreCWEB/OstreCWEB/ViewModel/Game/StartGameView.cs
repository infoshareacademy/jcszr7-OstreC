﻿using OstreCWEB.ViewModel.Characters;
using OstreCWEB.ViewModel.Identity;
using OstreCWEB.ViewModel.StoryBuilder;

namespace OstreCWEB.ViewModel.Game
{
    public class StartGameView
    {
        //This class will be merged with story reader next week. 
       public UserView User { get; set; } 
       public List<PlayableCharacterRow> OtherUsersCharacters { get; set; }
       public List<StoryView> OtherUsersStories { get; set; }
       public UserParagraphView UserParagraph { get; set; } 
        public PlayableCharacterView ActiveCharacter { get; set; }
        public StoryView ActiveStory { get; set; }
       public StartGameView( )
        {
            User = new UserView();
            OtherUsersCharacters = new List<PlayableCharacterRow>();
            OtherUsersStories = new List<StoryView>();
        }
    }
}