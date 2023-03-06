using OstreCWeb.DomainModels;

namespace OstreCWEB.DomainModels.StoryModels.Properties
{
    public class FightProp : IEntityBase
    {
        // General
        public int Id { get; set; }
        public List<EnemyInParagraph> ParagraphEnemies { get; set; }

        // Db relations properties
        public int ParagraphId { get; set; }
        public Paragraph Paragraph { get; set; }
    }
}
