using OstreCWEB.Data.Repository.Characters.CoreClasses;
using OstreCWEB.Data.Repository.Characters.Enums;

namespace OstreCWEB.Data.Repository.Characters
{
    public class CharacterRepository
    {
        private static int _totalAttributePoints;

        public static List<PlayableCharacter> characters = new List<PlayableCharacter>
        {
            new PlayableCharacter
            {
                CharacterId = 1,
                CharacterName = "AdminCharacter",
                CurrentHealthPoints = 10,
                Level = 1,
                Alignment = "Good",                    
                //EquippedArmor = Items.First(c=>c.CharacterId==1),
                //EquippedWeapon = Items.First(c=>c.CharacterId==2),
                //EquippedSecondaryWeapon =Items.First(c =>c.CharacterId ==4),
                //Inventory = new Item[5],
                //AllAvailableActions = new List<CharacterActions>(),
                Strength = 16,
                Dexterity = 14,
                Constitution = 10,
                Intelligence = 15,
                Wisdom = 12,
                Charisma = 2,
                //Race = PlayableRaces.FirstOrDefault(r=>r.ID ==1),
                //UserId = Users.FirstOrDefault(u=>u.CharacterId == 1).CharacterId,
                //CharacterClass =PlayableCharacterClasses.FirstOrDefault(c=>c.ID ==1)
                TotalAttributePoints = 69

            }
        };

        public List<PlayableCharacter> GetAll()
        {
            return characters;
        } 
        public List<PlayableRace> GetRace()
        {
            return playableRaces;
        }
        public List<PlayableCharacterClass> GetClass()
        {
            return characterClass;
        }
        public PlayableCharacter GetById(int id)
        {
            return characters.FirstOrDefault(c => c.CharacterId == id);
        }
        private int RollDice(int maxValue = 7)
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
            _totalAttributePoints += sum;
            return sum;
        }
        public void Update(PlayableCharacter model, string operation, AbilityScores attribute)
        {
            var zero = 0;
            var minValue = 0;
            var maxValue = 18;
            var calc = GetById(model.CharacterId);
            if (attribute == AbilityScores.Strength)
            {
                if (operation.ToLower() == "up")
                {
                    if (calc.Strength == maxValue || calc.AvailableAttributePoints == zero)
                        return;
                    calc.Strength = ++calc.Strength;
                    calc.AvailableAttributePoints = --calc.AvailableAttributePoints;
                }
                if (calc.Strength == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    calc.Strength = --calc.Strength;
                    calc.AvailableAttributePoints = ++calc.AvailableAttributePoints;
                }
            }
            if (attribute == AbilityScores.Dexterity)
            {
                if (operation.ToLower() == "up")
                {
                    if (calc.Dexterity == maxValue || calc.AvailableAttributePoints == zero)
                        return;
                    calc.Dexterity = ++calc.Dexterity;
                    calc.AvailableAttributePoints = --calc.AvailableAttributePoints;
                }
                if (calc.Dexterity == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    calc.Dexterity = --calc.Dexterity;
                    calc.AvailableAttributePoints = ++calc.AvailableAttributePoints;
                }

            }
            if (attribute == AbilityScores.Constitution)
            {
                if (operation.ToLower() == "up")
                {
                    if (calc.Constitution == maxValue || calc.AvailableAttributePoints == zero)
                        return;
                    calc.Constitution = ++calc.Constitution;
                    calc.AvailableAttributePoints = --calc.AvailableAttributePoints;
                }
                if (calc.Constitution == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    calc.Constitution = --calc.Constitution;
                    calc.AvailableAttributePoints = ++calc.AvailableAttributePoints;
                }

            }
            if (attribute == AbilityScores.Intelligence)
            {
                if (operation.ToLower() == "up")
                {
                    if (calc.Intelligence == maxValue || calc.AvailableAttributePoints == zero)
                        return;
                    calc.Intelligence = ++calc.Intelligence;
                    calc.AvailableAttributePoints = --calc.AvailableAttributePoints;
                }
                if (calc.Intelligence == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    calc.Intelligence = --calc.Intelligence;
                    calc.AvailableAttributePoints = ++calc.AvailableAttributePoints;
                }

            }
            if (attribute == AbilityScores.Wisdom)
            {
                if (operation.ToLower() == "up")
                {
                    if (calc.Wisdom == maxValue || calc.AvailableAttributePoints == zero)
                        return;
                    calc.Wisdom = ++calc.Wisdom;
                    calc.AvailableAttributePoints = --calc.AvailableAttributePoints;
                }
                if (calc.Wisdom == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    calc.Wisdom = --calc.Wisdom;
                    calc.AvailableAttributePoints = ++calc.AvailableAttributePoints;
                }

            }
            if (attribute == AbilityScores.Charisma)
            {
                if (operation.ToLower() == "up")
                {
                    if (calc.Charisma == maxValue || calc.AvailableAttributePoints == zero)
                        return;
                    calc.Charisma = ++calc.Charisma;
                    calc.AvailableAttributePoints = --calc.AvailableAttributePoints;
                }
                if (calc.Charisma == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    calc.Charisma = --calc.Charisma;
                    calc.AvailableAttributePoints = ++calc.AvailableAttributePoints;
                }
            }
        }

        public void RollAttributes(PlayableCharacter model)
        {
            _totalAttributePoints = 0;

            var calc = GetById(model.CharacterId);
            calc.Strength = RollDice();
            calc.Dexterity = RollDice();
            calc.Constitution = RollDice();
            calc.Intelligence = RollDice();
            calc.Wisdom = RollDice();
            calc.Charisma = RollDice();

            calc.AvailableAttributePoints = 0;
            calc.TotalAttributePoints = _totalAttributePoints;
        }

        public List<PlayableCharacterClass> characterClass = new List<PlayableCharacterClass>()
        {
            new PlayableCharacterClass{
                ID = 1,
                ClassName = CharClasses.Barbarian.ToString(),
            },
            new PlayableCharacterClass{
                ID = 2,
                ClassName = CharClasses.Bard.ToString(),
            },
            new PlayableCharacterClass{
                ID = 3,
                ClassName = CharClasses.Cleric.ToString(),
            },
            new PlayableCharacterClass{
                ID = 4,
                ClassName = CharClasses.Druid.ToString(),
            },
        };

        public List<PlayableRace> playableRaces = new List<PlayableRace>()
        {
            new PlayableRace{
                PlayableRaceId = 1,
                RaceName = Races.Dwarf.ToString(),
                Constitution = 2,
            },
            new PlayableRace{
                PlayableRaceId = 2,
                RaceName = Races.Elf.ToString(),
                Wisdom = 1,
            },//niziolek
            new PlayableRace{
                PlayableRaceId = 3,
                RaceName = Races.Halfling.ToString(),
                Dexterity = 2,
            },
            new PlayableRace{
                PlayableRaceId = 4,
                RaceName = Races.Human.ToString(),
                Strength = 1,
                Dexterity= 1,
                Constitution = 1,
                Intelligence = 1,
                Wisdom= 1,
                Charisma = 1,
            },//drakon
            new PlayableRace{
                PlayableRaceId = 5,
                RaceName = Races.Dragonborn.ToString(),
                Strength = 2,
                Charisma = 1
            },
            new PlayableRace{
                PlayableRaceId = 6,
                RaceName = Races.Gnome.ToString(),
                Intelligence = 2
            },
            new PlayableRace{
                PlayableRaceId = 7,
                RaceName = Races.HalfElf.ToString(),
                Charisma = 2,
                Intelligence = 1,
                Wisdom= 1,
            },
            new PlayableRace{
                PlayableRaceId = 8,
                RaceName = Races.HalfOrc.ToString(),
                Strength = 2,
                Constitution= 1,
            },//diabelstwo
            new PlayableRace{
                PlayableRaceId = 9,
                RaceName = Races.Tiefling.ToString(),
                Intelligence = 1,
                Charisma = 2,
            },
        };

        public List<CharacterSkills> skills = new List<CharacterSkills>()
        {
            new CharacterSkills{
                SkillsId = 1,
                SkillName = Skills.Athletics.ToString(),
            },
            new CharacterSkills{
                SkillsId = 2,
                SkillName = Skills.Acrobatics.ToString(),
            }
        };
    }
}
