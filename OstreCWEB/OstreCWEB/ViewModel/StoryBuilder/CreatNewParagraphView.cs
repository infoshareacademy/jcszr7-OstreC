using OstreCWEB.DomainModels.StoryModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.ViewModel.StoryBuilder
{
    public class CreatNewParagraphView
    {
        [Display(Name = "Paragraph Type")]
        public ParagraphType ParagraphType { get; set; }

        [Display(Name = "Stage Description")]
        public string StageDescription { get; set; }

        [Display(Name = "Restore Rest")]
        public bool RestoreRest { get; set; }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int AmountOfItems { get; set; }

        public int StoryId { get; set; }
        public Dictionary<int, string> Items { get; set; }
    }
}