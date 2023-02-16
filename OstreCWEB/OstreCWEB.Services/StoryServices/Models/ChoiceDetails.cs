using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.DomainModels.StoryModels.Properties;

namespace OstreCWEB.Services.StoryServices.Models
{
    public class ChoiceDetails
    {
        //Story
        public int StoryId { get; set; }
        public string NameOfStory { get; set; }
        public string DescriptionOfStory { get; set; }
        public int AmountOfParagraphs { get; set; }

        // Choice
        public Paragraph PreviousParagraph { get; set; }
        public Choice CurrentChoice { get; set; }
        public Paragraph NextParagraph { get; set; }

        //Settings
        public bool Delete { get; set; }
    }
}
