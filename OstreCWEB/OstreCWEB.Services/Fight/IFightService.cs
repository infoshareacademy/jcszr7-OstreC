using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Services.Fight
{
    public interface IFightService
    {
        public Task<FightInstance> InitializeFightAsync(int userId, UserParagraph gameInstance);
        public Task UpdateActiveActionAsync(int id, FightInstance fightInstance);
        public Task UpdateActiveTargetAsync(int id, FightInstance fightInstance);
        public Task SetActiveActionFromItem(FightInstance fightInstance, int id);
        public List<string> ReturnHistory(FightInstance model);
        public Ability GetActiveActions(FightInstance model);
        public Character GetActiveTarget(FightInstance model);
        public Character ResetActiveTarget(FightInstance model);
        public Task<bool> CommitActionAsync(int userId);
        //public FightInstance GetActiveFightInstance(int userId, int characterId);
        public Ability ResetActiveAction(FightInstance model);
        public Task DeleteFightInstanceAsync(int userId, FightInstance fightInstance);
        public Task<FightInstance> GetFightInstanceAsync();
    }
}