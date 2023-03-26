using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.StoryModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.Services.StoryService.ModelsDto
{
    public class CreatParagraphTestView
    {
        // ParagraphTest Properties
        [Display(Name = "Ability Score")]
        public AbilityScores AbilityScores { get; set; }

        [Display(Name = "Test Difficulty")]
        public TestDifficulty TestDifficulty { get; set; }

        // General
        [Display(Name = "Paragraph Type")]
        public ParagraphType ParagraphType { get; set; }

        [Display(Name = "Stage Description")]
        public string StageDescription { get; set; }

        [Display(Name = "Restore Rest")]
        public bool RestoreRest { get; set; }

        public int StoryId { get; set; }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int AmountOfItems { get; set; }
    }
}