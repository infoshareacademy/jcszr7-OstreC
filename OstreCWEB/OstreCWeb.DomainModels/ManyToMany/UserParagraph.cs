using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.DomainModels.StoryModels;
using System.ComponentModel.DataAnnotations;
using OstreCWeb.DomainModels;

namespace OstreCWEB.DomainModels.ManyToMany
{ 
    public class UserParagraph : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        //Many to many 
        public User User { get; set; }
        public Paragraph Paragraph { get; set; }
        public PlayableCharacter? ActiveCharacter { get; set; }
        public int? ActiveCharacterId { get; set; }
        public bool ActiveGame { get; set; }
        public bool Rest { get; set; }
    }
}
