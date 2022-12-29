using OstreCWEB.Data.Repository.Characters;
using OstreCWEB.Data.Repository.Characters.CoreClasses;

namespace OstreCWEB.Services.PlayableCharacter
{
    public class PlayableCharacterService
    {
        CharacterRepository _CharacterRepository = new CharacterRepository();
        public List<Data.Repository.Characters.CoreClasses.PlayableCharacter> GetAll()
        {
            return _CharacterRepository.GetAll();
        }
    }
}
