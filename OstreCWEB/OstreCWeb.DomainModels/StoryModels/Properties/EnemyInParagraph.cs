using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.DomainModels.StoryModels.Properties
{
    public class EnemyInParagraph
    {
        // General
        public int Id { get; set; }

        public int AmountOfEnemy { get; set; }

        // Db relations properties
        public int FightPropId { get; set; }
        public FightProp FightProp { get; set; }

        public int EnemyId { get; set; }
        public Enemy Enemy { get; set; }
    }
}
