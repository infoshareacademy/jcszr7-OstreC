using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.Services.StoryBuilder.ModelsDto
{
    public class ChoiceDetailsView
    {
        //Story
        public int StoryId { get; set; }

        public string NameOfStory { get; set; }
        public string DescriptionOfStory { get; set; }

        [Display(Name = "Amount Of Paragraphs")]
        public int AmountOfParagraphs { get; set; }

        // Choice
        public ParagraphElementView PreviousParagraph { get; set; }

        public CurrentChoiceView CurrentChoice { get; set; }
        public ParagraphElementView NextParagraph { get; set; }

        //Settings
        public bool Delete { get; set; }
    }
}