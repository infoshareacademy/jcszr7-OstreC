using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.ManyToMany;

namespace OstreCWEB.Repository.Repository.ManyToMany
{
    public interface IItemCharacterRepository<T> : IEntityBaseRepo<ItemCharacter> where T : ItemCharacter
    { 
        public Task AddRange(List<ItemCharacter> itemCharacter); 
    }
}
