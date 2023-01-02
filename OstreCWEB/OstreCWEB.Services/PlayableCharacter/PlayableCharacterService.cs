using OstreCWEB.Data.Repository.Characters;
using OstreCWEB.Data.Repository.Characters.CoreClasses;

namespace OstreCWEB.Services.PlayableCharacterService
{
    public class PlayableCharacterService
    {
        CharacterRepository _CharacterRepository = new CharacterRepository();
        public List<PlayableCharacter> GetAll()
        {
            return _CharacterRepository.characters;
        }
    }
}
