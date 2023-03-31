using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Services.Characters
{
    public interface IPlayableCharacterService
    {
        public Task<PlayableCharacter> GetById(int id);
        public void Create(PlayableCharacter playableCharacter);
        public Task<List<PlayableCharacter>> GetAll();
        public Task<List<PlayableCharacter>> GetAllTemplates(int id); 
        public List<PlayableRace> GetAllRaces();
        public List<PlayableClass> GetAllClasses();
        public void RollAttributes(PlayableCharacter model); 
        public List<string> GetAllNames();
        public string GetRaceDescription(int id);
        public string GetClassDescription(int id);
        public string GetRaceBonus(int id);
        public string GetClassBonus(int id);
        public int CalculateModifier(int value); 
        public int RollDice(int maxValue = 7);
        public bool Exists(int id);
    }
}
