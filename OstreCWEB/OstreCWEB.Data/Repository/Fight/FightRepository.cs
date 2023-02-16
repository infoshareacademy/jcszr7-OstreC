using OstreCWEB.DomainModels.Fight;

namespace OstreCWEB.Repository.Repository.Fight
{
    internal class FightRepository : IFightRepository
    {
        private static List<KeyValuePair<string, FightInstance>> FightInstances { get; set; } = new List<KeyValuePair<string, FightInstance>>();


        public bool Add(string userId, FightInstance fightInstance, out string operationResult)
        {
            if (FightInstances.Where(x => x.Key == userId).Count() < 5)
            {
                FightInstances.Add(new KeyValuePair<string, FightInstance>(userId, fightInstance));
                operationResult = "operation success";
                return true;
            }
            else
            {
                operationResult = "operation failed";
                return false;
            }
        }
        public FightInstance? GetById(string userId, int characterId)
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
        public bool Delete(string userId, int characterId, out string operationResult)
        {
            foreach (KeyValuePair<string, FightInstance> kvp in FightInstances)
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




