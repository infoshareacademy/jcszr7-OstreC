using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.Services.StoryServices.ModelsView
{
    public class StoryParagraphsView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Amount Of Paragraphs")]
        public int AmountOfParagraphs { get; set; }

        public List<ParagraphElementView> Paragraphs { get; set; }

        public int FirstParagraphId { get; set; }
    }
}