using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.StoryModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.Services.StoryService.ModelsDto
{
    public class CreatParagraphFightView
    {
        // Enemy Dictionary
        public Dictionary<int, string> Enemies { get; set; }

        // ParagraphFight Properties
        public int FirstEnemyId { get; set; }

        public int FirstAmountOfEnemy { get; set; }

        public int SecondEnemyId { get; set; }
        public int SecondAmountOfEnemy { get; set; }

        public int ThirdEnemyId { get; set; }
        public int ThirdAmountOfEnemy { get; set; }

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