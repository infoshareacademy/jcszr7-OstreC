using OstreCWEB.DomainModels.StoryModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.Services.StoryService.ModelsDto
{
    public class CreatNewParagraphView
    {
        [Display(Name = "Paragraph Type")]
        public ParagraphType ParagraphType { get; set; }

        [Display(Name = "Stage Description")]
        [Required(ErrorMessage = "Please provide Description")]
        [StringLength(100)]
        public string StageDescription { get; set; }

        [Display(Name = "Restore Rest")]
        public bool RestoreRest { get; set; }

        public int ItemId { get; set; }
        public string ItemName { get; set; }

        [Display(Name = "Amount of items")]
        [Required(ErrorMessage = "Please provide amount of items")]
        [Range(0, 3, ErrorMessage = "Please provide value from 0 to 3")]
        public int AmountOfItems { get; set; }

        public int StoryId { get; set; }
        public Dictionary<int, string> Items { get; set; }
    }
}