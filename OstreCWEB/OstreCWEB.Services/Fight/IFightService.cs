using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Services.Fight
{
    public interface IFightService
    {
        public Character ChooseTarget(int id);
        public Ability ChooseAction(int id);
        public Task InitializeFightAsync(string userId, UserParagraph gameInstance);
        public void UpdateActiveAction(Ability action);
        public void UpdateActiveTarget(Character character);
        public List<string> ReturnHistory();
        public Ability GetActiveActions();
        public Character GetActiveTarget();
        public Character ResetActiveTarget();
        public Task CommitAction(string userId);
        public FightInstance GetFightState(string userId, int characterId);
        public FightInstance GetActiveFightInstance(string userId, int characterId);
        public Task UpdateItemToRemove(int id);
        public Ability ResetActiveAction();
        public Task DeleteFightInstanceAsync(string userId);
    }
}