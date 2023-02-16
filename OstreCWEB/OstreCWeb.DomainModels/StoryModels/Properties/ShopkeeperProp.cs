namespace OstreCWEB.DomainModels.StoryModels.Properties
{
    public class ShopkeeperProp
    {
        // General
        public int Id { get; set; }

        // Db relations properties
        public int ParagraphId { get; set; }
        public Paragraph Paragraph { get; set; }
    }
}
