using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Repository.Repository.ManyToMany
{
    public interface IItemCharacterRepository
    {
        public Task<ItemCharacter> GetByCharacterId();
        public Task<ItemCharacter> GetAll();
        public Task AddRange(List<ItemCharacter> itemCharacter);
        public Task<ItemCharacter> Update();
        public Task<ItemCharacter> Delete();
    }
}
