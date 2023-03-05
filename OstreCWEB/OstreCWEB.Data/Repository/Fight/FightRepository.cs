using OstreCWEB.DomainModels.Fight;

namespace OstreCWEB.Repository.Repository.Fight
{
    internal class FightRepository : IFightRepository
    {
        private static List<KeyValuePair<int, FightInstance>> FightInstances { get; set; } = new List<KeyValuePair<int, FightInstance>>();


        public bool Add(int userId, FightInstance fightInstance, out string operationResult)
        {
            if (FightInstances.Where(x => x.Key == userId).Count() < 5)
            {
                FightInstances.Add(new KeyValuePair<int, FightInstance>(userId, fightInstance));
                operationResult = "operation success";
                return true;
            }
            else
            {
                operationResult = "operation failed";
                return false;
            }
        }
        public FightInstance? GetById(int userId, int characterId)
        {
            foreach (var fightInstanceDictionary in FightInstances)
            {
                if (fightInstanceDictionary.Key == userId && fightInstanceDictionary.Value.ActivePlayer.CharacterId == characterId)
                {
                    return fightInstanceDictionary.Value;
                }

            }
            return null;
        }

        public void DeleteLinkedItem(FightInstance fightInstance, int itemToDelete)
        {
            fightInstance.ActivePlayer.LinkedItems
            .Remove
            (
            fightInstance.ActivePlayer.LinkedItems.First(i => i.Id == itemToDelete)
            );
        }
        public bool Delete(int userId, int characterId, out string operationResult)
        {
            foreach (KeyValuePair<int, FightInstance> kvp in FightInstances)
            {
                if (kvp.Key == userId && kvp.Value.ActivePlayer.CharacterId == characterId)
                {
                    FightInstances.Remove(kvp);
                    operationResult = "operation success";
                    return true;
                }
                else
                {
                    operationResult = "FightInstance not found";
                    return false;
                }
            }
            operationResult = "Fight instance not found";
            return false;
        }
    }
}




