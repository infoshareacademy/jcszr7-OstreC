using OstreCWEB.DomainModels.CharacterModels;

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
        public Ability ActiveAction { get; set; }
        public bool IsItemToDelete { get; set; }
        public bool ActionGrantedByItem { get; set; }
        public int UserParagraphId { get; set; }
        public bool isPlayerFirst { get; set; }

        public int AiFirstTurnCounter { get; set; } = 1;
    }
}
