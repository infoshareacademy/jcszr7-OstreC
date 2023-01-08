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
        public List<PlayableRace> GetRace()
        {
            return _CharacterRepository.GetRace();
        }
        public List<PlayableCharacterClass> GetClass()
        {
            return _CharacterRepository.GetClass();
        }
        public List<CharacterSkills> GetSkills()
        {
            return _CharacterRepository.GetSkills();
        }
        public void Update(PlayableCharacter model, string operation, AbilityScores attribute)
        {
            _CharacterRepository.Update(model, operation, attribute);
        }
        public void UpdateSkill(CharacterSkills model, string operation, Skills skill)
        {
            _CharacterRepository.UpdateSkill(model, operation, skill);
        }

        public void RollAttributes(PlayableCharacter model)
        {
            _CharacterRepository.RollAttributes(model);
        }
    }
}
