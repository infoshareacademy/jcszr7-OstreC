using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.DomainModels.StoryModels;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.DomainModels.ManyToMany
{
    //It will be replaced by story reader mclasses most likely.
    public class UserParagraph
    {
        [Key]
        public int UserParagraphId { get; set; }
        //Many to many 
        public User User { get; set; }
        public Paragraph Paragraph { get; set; }
        public PlayableCharacter? ActiveCharacter { get; set; }
        public int? ActiveCharacterId { get; set; }
        public bool ActiveGame { get; set; }
        public bool Rest { get; set; }
    }
}
