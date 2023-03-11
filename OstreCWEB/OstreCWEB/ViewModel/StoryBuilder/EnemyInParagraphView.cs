namespace OstreCWEB.ViewModel.StoryBuilder
{
    public class EnemyInParagraphView
    {
        public int Id { get; set; }

        public string EnemyName { get; set; }
        public int AmountOfEnemy { get; set; }

        public int ParagraphId { get; set; }
        public int FightPropId { get; set; }
        public int EnemyId { get; set; }

        public Dictionary<int, string> Enemies { get; set; }
    }
}