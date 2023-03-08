using OstreCWeb.DomainModels;
using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.StoryModels.Enums;

namespace OstreCWEB.DomainModels.StoryModels.Properties
{
    public class TestProp : IEntityBase
    {
        // General
        public int Id { get; set; }

        public AbilityScores AbilityScores { get; set; }
        public TestDifficulty TestDifficulty { get; set; }

        // Db relations properties
        public int ParagraphId { get; set; }
        public Paragraph Paragraph { get; set; }
    }
}
