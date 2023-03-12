using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.StoryModels;

namespace OstreCWeb.DomainModels.StoryModels.Properties
{
    public class ParagraphItem : IEntityBase
    {
        public int Id { get; set; }

        public int AmountOfItems { get; set; }

        // Db relations properties
        public int ItemId { get; set; }

        public Item Item { get; set; }

        public int ParagraphId { get; set; }
        public Paragraph Paragraph { get; set; }
    }
}