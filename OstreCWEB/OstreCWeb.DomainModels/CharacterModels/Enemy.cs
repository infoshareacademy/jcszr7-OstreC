using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.StoryModels.Properties;

namespace OstreCWEB.DomainModels.CharacterModels
{ 
    public class Enemy : Character
    {
        public Races NonPlayableRace { get; set; }

        public List<EnemyInParagraph> EnemyInParagraphs { get; set; } 
    }
}

