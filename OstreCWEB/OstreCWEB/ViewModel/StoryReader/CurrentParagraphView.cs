using OstreCWEB.DomainModels.StoryModels.Enums;

namespace OstreCWEB.ViewModel.StoryReader
{
    public class CurrentParagraphView
    {
        public ParagraphType ParagraphType { get; set; }
        public string StageDescription { get; set; }
        public List<CurrentChoicesView> Choices { get; set; }
        public List<ParagraphItemView> ParagraphItems { get; set; }
    }
}
