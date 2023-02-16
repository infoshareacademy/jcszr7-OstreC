namespace OstreCWEB.ViewModel.StoryBuilder
{
    public class CreatParagraphFightView
    {
        // Enemy Dictionary
        public Dictionary<int, string> Enemies { get; set; }

        // ParagraphFight Properties             
        public int FirstEnemyId { get; set; }
        public int FirstAmountOfEnemy { get; set; }

        public int SecondEnemyId { get; set; }
        public int SecondAmountOfEnemy { get; set; }

        public int ThirdEnemyId { get; set; }
        public int ThirdAmountOfEnemy { get; set; }

        // General
        public ParagraphType ParagraphType { get; set; }
        public string StageDescription { get; set; }
        public bool RestoreRest { get; set; }
        public int StoryId { get; set; }
    }
}
