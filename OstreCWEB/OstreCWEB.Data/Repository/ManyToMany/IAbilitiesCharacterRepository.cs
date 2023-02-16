using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Repository.Repository.ManyToMany
{
    public interface IAbilitiesCharacterRepository
    {
        public Task<AbilitiesCharacter> GetByCharacterId();
        public Task<AbilitiesCharacter> GetAll();
        public Task<AbilitiesCharacter> Create();
        public Task<AbilitiesCharacter> Update();
        public Task<AbilitiesCharacter> Delete();
    }
}
