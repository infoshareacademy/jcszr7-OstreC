﻿namespace OstreCWEB.Services.StoryServices.Models
{
    public class EnemyInParagraphService
    {
        public int Id { get; set; }
        public int AmountOfEnemy { get; set; }
        public string EnemyName { get; set; }

        public int FightPropId { get; set; }
        public int EnemyId { get; set; }
    }
}
