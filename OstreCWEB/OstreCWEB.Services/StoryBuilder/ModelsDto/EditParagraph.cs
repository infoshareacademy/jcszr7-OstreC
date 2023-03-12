using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.StoryModels.Enums;

namespace OstreCWEB.Services.StoryBuilder.Models
{
    public class EditParagraph
    {
        public int Id { get; set; }

        public ParagraphType ParagraphType { get; set; }
        public string StageDescription { get; set; }
        public bool RestoreRest { get; set; }

        // Paragraph Fight properties
        public List<EnemyInParagraphService> ParagraphEnemies { get; set; }

        // ParagraphTest Properties
        public AbilityScores AbilityScores { get; set; }
        public TestDifficulty TestDifficulty { get; set; }

        // Db relations properties
        //public List<ParagraphItem> ParagraphItems { get; set; }
        public int FightPropId { get; set; }
        public int StoryId { get; set; }
    }
}
