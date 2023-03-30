using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWeb.DomainModels;

namespace OstreCWEB.Services.Characters
{
    public class PlayableCharacterService : IPlayableCharacterService
    {
        private readonly IPlayableCharacterRepository<PlayableCharacter> _playableCharacterRepository;
        private readonly ICharacterClassRepository<PlayableClass> _characterClassRepository;
        private readonly ICharacterRaceRepository<PlayableRace> _characterRaceRepository;

        public PlayableCharacterService(
            IPlayableCharacterRepository<PlayableCharacter> characterRepository, 
            ICharacterClassRepository<PlayableClass> characterClassRepository,
            ICharacterRaceRepository<PlayableRace> characterRaceRepository)
        {
            _playableCharacterRepository = characterRepository;
            _characterClassRepository = characterClassRepository;
            _characterRaceRepository = characterRaceRepository;
        }
        public bool Exists(int id)
        {
            return _playableCharacterRepository.Exists(id);
        } 
        public void Create(PlayableCharacter playableCharacter)
        {
            playableCharacter.CharacterClass = _characterClassRepository.GetById(playableCharacter.PlayableClassId);
            playableCharacter.Race = _characterRaceRepository.GetById(playableCharacter.RaceId);
            playableCharacter.Strenght = playableCharacter.Strenght + playableCharacter.CharacterClass.StrengthBonus + playableCharacter.Race.StrengthBonus;
            playableCharacter.Dexterity = playableCharacter.Dexterity + playableCharacter.CharacterClass.DexterityBonus + playableCharacter.Race.DexterityBonus;
            playableCharacter.Constitution = playableCharacter.Constitution + playableCharacter.CharacterClass.ConstitutionBonus + playableCharacter.Race.ConstitutionBonus;
            playableCharacter.Intelligence = playableCharacter.Intelligence + playableCharacter.CharacterClass.IntelligenceBonus + playableCharacter.Race.IntelligenceBonus;
            playableCharacter.Wisdom = playableCharacter.Wisdom + playableCharacter.CharacterClass.WisdomBonus + playableCharacter.Race.WisdomBonus;
            playableCharacter.Charisma = playableCharacter.Charisma + playableCharacter.CharacterClass.CharismaBonus + playableCharacter.Race.CharismaBonus;
            playableCharacter.CurrentHealthPoints = playableCharacter.MaxHP();
            playableCharacter.MaxHealthPoints = playableCharacter.MaxHP();
            playableCharacter.IsTemplate = true;
            
            _playableCharacterRepository.Add(playableCharacter);
        }
        public Task<List<PlayableCharacter>> GetAll()
        {
            return _playableCharacterRepository.GetAllTemplatesAsync();
        }
        /// <summary>
        /// Gets all playable characters except those belonging to a given  user
        /// </summary>
        /// <param name="userCharacters"></param>
        /// <returns></returns>
        public async Task<List<PlayableCharacter>> GetAllTemplates(int userId)
        {
            return await _playableCharacterRepository.GetAllTemplatesExceptAsync(userId);
        }
        public async Task<PlayableCharacter> GetById(int id)
        {
            return await _playableCharacterRepository.GetByIdAsync(id);
        }  
        public List<PlayableRace> GetAllRaces()
        {
            return _characterRaceRepository.GetAll();
        }
        public List<PlayableClass> GetAllClasses()
        {
            return _characterClassRepository.GetAll();
        }
        public void RollAttributes(PlayableCharacter model)
        {
            var calc = model;
            calc.Strenght = RollDice();
            calc.Dexterity = RollDice();
            calc.Constitution = RollDice();
            calc.Intelligence = RollDice();
            calc.Wisdom = RollDice();
            calc.Charisma = RollDice();
        }
         
        #region quickAutisticMethod
        public int RollDice(int maxValue = 7)
        {
            int[] rolls = new int[4];

            Random rng = new Random();
            for (int i = 0; i < rolls.Length; i++)
            {
                rolls[i] = rng.Next(1, maxValue);
            }
            Array.Sort(rolls);
            Array.Reverse(rolls);

            int sum = rolls.Take(3).Sum();
            return sum;
        }
        public int CalculateModifier(int value)
        {
            List<int> numbers = new List<int>() {
                   -5,-4,-4,-3,-3,-2,-2,-1,-1, 0,
                    0, 1, 1, 2, 2, 3, 3, 4, 4, 5,
                    5, 6, 6, 7, 7, 8, 8, 9, 9, 10 };

            return numbers.First(x => x == numbers[value - 1]);
        } 
        public List<string> GetAllNames()
        {
            return new List<string>()
            {
                "Aiden", "Ethan", "Jacob", "Michael", "William",
                "Alexander", "Daniel", "Matthew", "Nicholas", "Christopher",
                "Andrew", "Brandon", "Caleb", "David", "Elijah",
                "Isaac", "Gabriel", "Josiah", "Luke", "Noah"
            };
        }
        public string GetRaceDescription(int id)
        {
            var raceList = _characterRaceRepository.GetAll();
            return raceList[id].RaceDescription;
        }
        public string GetClassDescription(int id)
        {
            var classList = _characterClassRepository.GetAll();
            return classList[id].ClassDescription;
        }

        public string GetRaceBonus(int id)
        {
            var listWithBonus = _characterRaceRepository.GetAll();
            var bonusMessage = GetBonus(listWithBonus, id);
            return bonusMessage;
        }

        public string GetClassBonus(int id)
        {
            var listWithBonus = _characterClassRepository.GetAll();
            var bonusMessage = GetBonus(listWithBonus, id);
            return bonusMessage;
        }

        private string GetBonus(dynamic listWithBonus, int id)
        {
            var bonusMessage = "";

            var strength = listWithBonus[id].StrengthBonus;
            var dexterity = listWithBonus[id].DexterityBonus;
            var constitution = listWithBonus[id].ConstitutionBonus;
            var intelligence = listWithBonus[id].IntelligenceBonus;
            var wisdom = listWithBonus[id].WisdomBonus;
            var charisma = listWithBonus[id].CharismaBonus;

            if (strength != 0) { bonusMessage += $"Strength: {strength} "; }
            if (dexterity != 0) { bonusMessage += $"Dexterity: {dexterity} "; }
            if (constitution != 0) { bonusMessage += $"Constitution: {constitution} "; }
            if (intelligence != 0) { bonusMessage += $"Intelligence: {intelligence} "; }
            if (wisdom != 0) { bonusMessage += $"Wisdom: {wisdom} "; }
            if (charisma != 0) { bonusMessage += $"Charisma: {charisma} "; }

            return bonusMessage;
        }
        #endregion
    }
}
