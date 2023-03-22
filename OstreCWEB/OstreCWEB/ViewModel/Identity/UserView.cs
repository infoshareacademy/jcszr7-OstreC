using OstreCWEB.ViewModel.Characters;
using OstreCWEB.Services.StoryService.ModelsDto;
using System.ComponentModel;

namespace OstreCWEB.ViewModel.Identity
{
    public class UserView
    {
        [DisplayName("Your Characters")]
        public List<PlayableCharacterRow> CharactersCreated { get; set; }

        [DisplayName("Your Stories")]
        public List<StoryView> StoriesCreated { get; set; }

        [DisplayName("Your Saved Games")]
        public List<UserParagraphView> UserParagraphs { get; set; }

        //
        public string Name { get; set; }

        public DateTime Created { get; set; }
    }
}