﻿using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Characters;

namespace OstreCWEB.DomainModels.Fight
{
    public class FightInstance
    {
        public List<string> FightHistory { get; set; }
        public List<Enemy> ActiveEnemies { get; set; }
        public PlayableCharacter ActivePlayer { get; set; }
        public Character ActiveTarget { get; set; }
        public int TurnNumber { get; set; }
        public int PlayerActionCounter { get; set; }
        public bool CombatFinished { get; set; }
        public bool PlayerWon { get; set; }
        public int ItemToDeleteId { get; set; }
        public Abilities ActiveAction { get; set; }
        public bool IsItemToDelete { get; set; }
        public bool ActionGrantedByItem { get; set; }
        public int UserParagraphId { get; set; }
    }
}
