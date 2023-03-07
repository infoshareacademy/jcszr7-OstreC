using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters.Interfaces
{
    public interface IStatusRepository<T> : IEntityBaseRepo<Status> where T : Status
    {  
    }
}
