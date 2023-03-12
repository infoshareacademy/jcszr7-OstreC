using OstreCWEB.DomainModels.StoryModels;

namespace OstreCWEB.Services.StoryServices.Models
{
    public class ParagraphDetails
    {
        public int StoryId { get; set; }
        public string NameOfStory { get; set; }
        public string DescriptionOfStory { get; set; }
        public int AmountOfParagraphs { get; set; }

        public List<ParagraphWithCoice> PreviousParagraphs { get; set; } = new List<ParagraphWithCoice>();
        public Paragraph CurrentParagraph { get; set; }
        public List<ParagraphWithCoice> NextParagraphs { get; set; } = new List<ParagraphWithCoice>();

        public bool CreatChoice { get; set; }
        public bool Delete { get; set; }
    }
}
