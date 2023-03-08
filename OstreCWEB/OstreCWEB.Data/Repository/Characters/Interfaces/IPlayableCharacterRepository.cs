using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters.Interfaces
{
    public interface IPlayableCharacterRepository<T> : IEntityBaseRepo<T> where T:class
    { 
        public Task<List<PlayableCharacter>> GetAllTemplatesAsync();
        public Task<List<PlayableCharacter>> GetAllTemplatesExceptAsync(int id);
        public  Task<PlayableCharacter> GetByIdNoTrackingAsync(int characterTemplateId);
    }
}
