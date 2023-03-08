using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Repository.Repository.ManyToMany
{
    //Due to composite key it can't implement IEntityBaseRepo.
    public   interface IAbilitiesCharacterRepository
    {
        public Task<AbilitiesCharacter> GetByCharacterId();
        public Task<AbilitiesCharacter> GetAll();
        public Task<AbilitiesCharacter> Create();
        public Task<AbilitiesCharacter> Update();
        public Task<AbilitiesCharacter> Delete();
    }
}
