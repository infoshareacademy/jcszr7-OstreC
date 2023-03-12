using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.Services.StoryBuilder.ModelsDto
{
    public class StoryView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FirstParagraphId { get; set; }

        [Display(Name = "Amount Of Paragraphs")]
        public int AmountOfParagraphs { get; set; }
    }
}