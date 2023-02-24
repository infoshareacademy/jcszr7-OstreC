using OstreCWEB.DomainModels.Fight;

namespace OstreCWEB.Repository.Repository.Fight
{
    public interface IFightRepository
    {
        public bool Add(string userId, FightInstance fightInstance, out string operationResult);
        public FightInstance GetById(string userId, int characterId); 
        public void DeleteLinkedItem(FightInstance fightInstance, int itemToDelete);
        public bool Delete(string userId, int characterId, out string operationResult);
    }
}
