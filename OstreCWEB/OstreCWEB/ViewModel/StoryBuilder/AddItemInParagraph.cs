namespace OstreCWEB.ViewModel.StoryBuilder
{
    public class AddItemInParagraph
    {
        public string ItemName { get; set; }
        public int AmountOfItems { get; set; }

        public int ParagraphId { get; set; }
        public int ItemId { get; set; }

        public Dictionary<int, string> Items { get; set; }
    }
}