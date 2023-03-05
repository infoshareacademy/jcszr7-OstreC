using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Services.Characters
{
    public interface IPlayableCharacterService
    {
        public Task<PlayableCharacter> GetById(int id);
        public Task Create(PlayableCharacter playableCharacter);
        public Task<List<PlayableCharacter>> GetAll();
        public Task<List<PlayableCharacter>> GetAllTemplates(int id);
        public Task Add(Character charater);
        public Task Update(Character charater);
        public Task Remove(Character charater);
        #region
        public void CreateNew(PlayableCharacter model);
        public List<PlayableRace> GetAllRaces();
        public List<PlayableClass> GetAllClasses();
        public void RollAttributes(PlayableCharacter model);
        public Task<string> GetClassNameById(int id);
        public List<string> GetAllNames();
        public string GetRaceDescription(int id);
        public string GetClassDescription(int id);
        public int CalculateModifier(int value);
        #endregion
        public int RollDice(int maxValue = 7);
        public bool Exists(int id);
    }
}
