using Microsoft.AspNetCore.Identity;
using OstreCWEB.DomainModels.CharacterModels; 
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.DomainModels.StoryModels;

namespace OstreCWEB.DomainModels.Identity
{
    public class User : IdentityUser
    {
        //Ef Config
        public List<PlayableCharacter> CharactersCreated { get; set; }
        public List<Story> StoriesCreated { get; set; }
        public List<UserParagraph> UserParagraphs { get; set; }
        // 

        public string Name { get; set; }
        public DateTime Created { get; set; }
        public int StoriesCompletedTotal { get; set; }
        public int DamageDealt { get; set; }
        public int DamageReceived { get; set; }

        public User() : base()
        {
            CharactersCreated = new List<PlayableCharacter>();
            StoriesCreated = new List<Story>();
            UserParagraphs = new List<UserParagraph>();
        }
    }
}
