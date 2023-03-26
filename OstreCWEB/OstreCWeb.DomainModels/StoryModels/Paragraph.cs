using OstreCWeb.DomainModels;
using OstreCWeb.DomainModels.StoryModels.Properties;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.DomainModels.StoryModels.Enums;
using OstreCWEB.DomainModels.StoryModels.Properties;

namespace OstreCWEB.DomainModels.StoryModels
{
    public class Paragraph : IEntityBase
    {
        // General
        public int Id { get; set; }

        public ParagraphType ParagraphType { get; set; }
        public string StageDescription { get; set; }
        public bool RestoreRest { get; set; }

        // Paragraph type properties
        public FightProp? FightProp { get; set; }

        public DialogProp? DialogProp { get; set; }
        public TestProp? TestProp { get; set; }
        public ShopkeeperProp? ShopkeeperProp { get; set; }

        // Choice options
        public List<Choice> Choices { get; set; } = new List<Choice>();

        // Db relations properties
        public List<UserParagraph> UserParagraphs { get; set; }

        public List<ParagraphItem> ParagraphItems { get; set; }
        public int StoryId { get; set; }
        public Story Story { get; set; }

        public int GetAmountOfChoices()
        {
            return Choices.Count();
        }
    }
}