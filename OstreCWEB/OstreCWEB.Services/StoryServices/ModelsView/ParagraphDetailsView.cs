using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.Services.StoryServices.ModelsView
{
    public class ParagraphDetailsView
    {
        public int StoryId { get; set; }
        public string NameOfStory { get; set; }
        public string DescriptionOfStory { get; set; }

        [Display(Name = "Amount Of Paragraphs")]
        public int AmountOfParagraphs { get; set; }

        public List<ParagraphWithCoiceView> PreviousParagraphs { get; set; } = new List<ParagraphWithCoiceView>();
        public ParagraphElementView CurrentParagraph { get; set; }
        public List<ParagraphWithCoiceView> NextParagraphs { get; set; } = new List<ParagraphWithCoiceView>();

        public bool CreatChoice { get; set; }
        public bool Delete { get; set; }
    }
}