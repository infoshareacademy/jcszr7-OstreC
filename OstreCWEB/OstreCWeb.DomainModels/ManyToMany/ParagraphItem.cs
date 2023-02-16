using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.StoryModels;

namespace OstreCWEB.DomainModels.ManyToMany
{
    public class ParagraphItem
    {
        public int AmountOfItems { get; set; }

        // Db relations properties
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int ParagraphId { get; set; }
        public Paragraph Paragraph { get; set; }
    }
}
