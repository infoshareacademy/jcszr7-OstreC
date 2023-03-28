using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.ManyToMany;
using System.ComponentModel.DataAnnotations;
using OstreCWeb.DomainModels;
using OstreCWeb.DomainModels.StoryModels.Properties;

namespace OstreCWEB.DomainModels.CharacterModels
{
    public class Item : IEntityBase
    {
        //Ef Config
        [Key]
        public int Id { get; set; }
        public List<ItemCharacter> LinkedCharacters { get; set; }
        public List<ParagraphItem> ParagraphItems { get; set; }
        public Ability? Ability { get; set; }
        public int? AbilityId { get; set; }
        public int? PlayableClassId { get; set; }
        public PlayableClass? PlayableClass { get; set; }
        // 
        public ItemType ItemType { get; set; }
        public int? ArmorClass { get; set; }
        public string Name { get; set; }
        public bool DeleteOnUse { get; set; }
        public Item() { }
    }
}
