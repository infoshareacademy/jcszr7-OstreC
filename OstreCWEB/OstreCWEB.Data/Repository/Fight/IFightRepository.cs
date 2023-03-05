using OstreCWEB.DomainModels.Fight;

namespace OstreCWEB.Repository.Repository.Fight
{
    public interface IFightRepository
    {
        public bool Add(int userId, FightInstance fightInstance, out string operationResult);
        public FightInstance GetById(int userId, int characterId); 
        public void DeleteLinkedItem(FightInstance fightInstance, int itemToDelete);
        public bool Delete(int userId, int characterId, out string operationResult);
    }
}
