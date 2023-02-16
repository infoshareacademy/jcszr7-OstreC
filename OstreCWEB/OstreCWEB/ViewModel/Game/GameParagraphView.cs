using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.ViewModel.Game
{
    public class GameParagraphView
    {
        public int Id { get; set; }

        [Display(Name = "Paragraph Type")]
        public ParagraphType ParagraphType { get; set; }

        [Display(Name = "Stage Description")]
        public string StageDescription { get; set; }

        [Display(Name = "Amount Of Choices")]
        public int AmountOfChoices { get; set; }
        public int StoryId { get; set; }
    }
}
