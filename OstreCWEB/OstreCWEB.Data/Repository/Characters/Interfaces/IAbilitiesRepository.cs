using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters.Interfaces
{
    public interface IAbilitiesRepository<T> : IEntityBaseRepo<Ability> where T : Ability

    {  
    public Task DeleteAsync(int id);
    }
}
