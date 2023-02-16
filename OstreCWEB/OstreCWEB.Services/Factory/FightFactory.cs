using OstreCWEB.Data.Repository.Fight;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Services.Factory
{
    internal class FightFactory : IFightFactory
    {
        private IFightRepository _fightRepository;

        public FightFactory(IFightRepository fightRepository)
        {
            _fightRepository = fightRepository;
        }
        public List<Character> InitializeActions(List<Character> characterList)
        {
            foreach (var character in characterList)
            {
                foreach (var linkedAction in character.LinkedActions)
                {
                    character.InnateActions.Add(linkedAction.CharacterAction);
                }
                foreach (var linkedItems in character.LinkedItems)
                {
                    if (linkedItems.IsEquipped) { character.EquippedItems.Add(linkedItems.Item); }
                    else { character.Inventory.Add(linkedItems.Item); }
                }
            }
            return characterList;
        }
        public FightInstance BuildNewFightInstance(UserParagraph gameInstance, List<Enemy> enemiesList)
        {
            var characterList = new List<Character>();
            var fightInstance = new FightInstance();
            fightInstance.IsItemToDelete = false;
            fightInstance.ActionGrantedByItem = false;
            fightInstance.TurnNumber = 1;
            fightInstance.ActivePlayer = gameInstance.ActiveCharacter;
            fightInstance.ActiveEnemies = enemiesList;
            fightInstance.ActivePlayer.CombatId = 1;
            for (int i = 0; i < fightInstance.ActiveEnemies.Count; i++)
            {
                //+2 because we have to start at 0 and player combat id by default is 1. 
                fightInstance.ActiveEnemies[i].CombatId = i + 2;
            }
            characterList.Add((Character)fightInstance.ActivePlayer);
            fightInstance.ActiveEnemies.ForEach(enemy => characterList.Add(enemy));
            InitializeActions(characterList);
            fightInstance.PlayerActionCounter = 2;
            fightInstance.FightHistory = new List<string>();
            fightInstance.UserParagraphId = gameInstance.UserParagraphId;
            return fightInstance;
        }
    }
}
