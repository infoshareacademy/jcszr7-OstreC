using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters.Interfaces
{
    public interface ICharacterClassRepository<T> : IEntityBaseRepo<PlayableClass> where T : PlayableClass
    {
        
    }
}
