using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters.Interfaces
{
    public interface ICharacterRaceRepository<T> : IEntityBaseRepo<PlayableRace> where T : PlayableRace
    { 
    }
}
