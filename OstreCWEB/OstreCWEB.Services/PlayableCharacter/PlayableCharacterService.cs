using OstreCWEB.Data.Repository.Characters;
using OstreCWEB.Data.Repository.Characters.CoreClasses;
using OstreCWEB.Data.Repository.Characters.Enums;

namespace OstreCWEB.Services.PlayableCharacterService
{
    public class PlayableCharacterService
    {
        CharacterRepository _CharacterRepository = new CharacterRepository();
        public List<PlayableCharacter> GetAll()
        {
            return _CharacterRepository.GetAll();
        }
        public void Update(PlayableCharacter model, string operation, AbilityScores attribute)
        {
            _CharacterRepository.Update(model, operation, attribute);
        }

        public void RollAttributes(PlayableCharacter model)
        {
            _CharacterRepository.RollAttributes(model);
        }
    }
}
