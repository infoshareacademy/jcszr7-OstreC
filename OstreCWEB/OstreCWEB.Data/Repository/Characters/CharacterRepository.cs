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
        public List<CharacterSkills> GetSkills()
        {
            return skills;
        }
        public PlayableCharacter GetById(int id)
        {
            return characters.FirstOrDefault(c => c.CharacterId == id);
        }
        public CharacterSkills GetSkillsById(int id)
        {
            return skills.FirstOrDefault(s => s.CharacterId == id);
        }
        public PlayableCharacterClass GetClassById(int id)
        {
            return characterClass.FirstOrDefault(c => c.PlayableCharacterId == id);
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
        public void UpdateSkill(CharacterSkills model, string operation, Skills skill)
        {
            var zero = 0;
            var minValue = 0;
            var maxValue = 1;
            var player = GetSkillsById(model.CharacterId);
            if (skill == Skills.Athletics)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Athletics == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Athletics = ++player.Athletics;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Athletics == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Athletics = --player.Athletics;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
            if (skill == Skills.Acrobatics)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Acrobatics == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Acrobatics = ++player.Acrobatics;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Acrobatics == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Acrobatics = --player.Acrobatics;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }            
            if (skill == Skills.SleightOfHand)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.SleightOfHand == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.SleightOfHand = ++player.SleightOfHand;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.SleightOfHand == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.SleightOfHand = --player.SleightOfHand;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }

            }
            if (skill == Skills.Stealth)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Stealth == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Stealth = ++player.Stealth;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Stealth == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Stealth = --player.Stealth;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }

            }
            if (skill == Skills.Arcana)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Arcana == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Arcana = ++player.Arcana;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Arcana == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Arcana = --player.Arcana;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }

            }
            if (skill == Skills.History)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.History == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.History = ++player.History;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.History == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.History = --player.History;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
            if (skill == Skills.Investigation)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Investigation == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Investigation = ++player.Investigation;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Investigation == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Investigation = --player.Investigation;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
            if (skill == Skills.Nature)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Nature == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Nature = ++player.Nature;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Nature == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Nature = --player.Nature;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
            if (skill == Skills.Religion)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Religion == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Religion = ++player.Religion;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Religion == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Religion = --player.Religion;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
            if (skill == Skills.AnimalHandling)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.AnimalHandling == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.AnimalHandling = ++player.AnimalHandling;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.AnimalHandling == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.AnimalHandling = --player.AnimalHandling;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
            if (skill == Skills.Insight)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Insight == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Insight = ++player.Insight;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Insight == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Insight = --player.Insight;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
            if (skill == Skills.Medicine)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Medicine == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Medicine = ++player.Medicine;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Medicine == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Medicine = --player.Medicine;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
            if (skill == Skills.Perception)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Perception == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Perception = ++player.Perception;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Perception == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Perception = --player.Perception;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
            if (skill == Skills.Survival)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Survival == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Survival = ++player.Survival;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Survival == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Survival = --player.Survival;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
            if (skill == Skills.Deception)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Deception == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Deception = ++player.Deception;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Deception == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Deception = --player.Deception;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
            if (skill == Skills.Intimidation)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Intimidation == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Intimidation = ++player.Intimidation;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Intimidation == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Intimidation = --player.Intimidation;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
            if (skill == Skills.Performance)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Performance == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Performance = ++player.Performance;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Performance == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Performance = --player.Performance;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
            if (skill == Skills.Persuasion)
            {
                if (operation.ToLower() == "up")
                {
                    if (player.Persuasion == maxValue || player.AvailableSkillPoints == zero)
                        return;
                    player.Persuasion = ++player.Persuasion;
                    player.AvailableSkillPoints = --player.AvailableSkillPoints;
                }
                if (player.Persuasion == minValue)
                    return;
                if (operation.ToLower() == "down")
                {
                    player.Persuasion = --player.Persuasion;
                    player.AvailableSkillPoints = ++player.AvailableSkillPoints;
                }
            }
        }
        public void UpdateClass(PlayableCharacterClass model, string className)
        {
            var playerClass = GetClassById(model.PlayableCharacterId);
            if (className == CharClasses.Barbarian.ToString())
            {
                playerClass.ClassName = CharClasses.Barbarian.ToString();
            }
            if (className == CharClasses.Bard.ToString())
            {
                playerClass.ClassName = CharClasses.Bard.ToString();
            }
            if (className == CharClasses.Cleric.ToString())
            {
                playerClass.ClassName = CharClasses.Cleric.ToString();
            }
            if (className == CharClasses.Druid.ToString())
            {
                playerClass.ClassName = CharClasses.Druid.ToString();
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

        public static List<PlayableCharacterClass> characterClass = new List<PlayableCharacterClass>()
        {
            new PlayableCharacterClass{
                ID = 1,
                ClassName = CharClasses.Barbarian.ToString(),
            },
            //new PlayableCharacterClass{
            //    ID = 2,
            //    ClassName = CharClasses.Bard.ToString(),
            //},
            //new PlayableCharacterClass{
            //    ID = 3,
            //    ClassName = CharClasses.Cleric.ToString(),
            //},
            //new PlayableCharacterClass{
            //    ID = 4,
            //    ClassName = CharClasses.Druid.ToString(),
            //},
        };

        public static List<PlayableRace> playableRaces = new List<PlayableRace>()
        {
            new PlayableRace{
                PlayableRaceId = 1,
                RaceName = Races.Dwarf.ToString(),
                Constitution = 2,
            },
            new PlayableRace{
                PlayableRaceId = 2,
                RaceName = Races.Elf.ToString(),
                Dexterity = 2,
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
                Intelligence = 1, //extra random
                Wisdom= 1, //extra random
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

        public static List<CharacterSkills> skills = new List<CharacterSkills>()
        {//global
            new CharacterSkills{
                CharacterId = 1,
                AvailableSkillPoints = 4,
                Athletics = 0,
                Acrobatics = 0,
                SleightOfHand = 0,
                Stealth = 0,
                Arcana = 0,
                History = 0,
                Investigation = 0,
                Nature = 0,
                Religion = 0,
                AnimalHandling = 0,
                Insight = 0,
                Medicine = 0,
                Perception = 0,
                Survival = 0,
                Deception = 0,
                Intimidation = 0,
                Performance = 0,
                Persuasion = 0,
            },//barbarian
            new CharacterSkills{
                CharacterId = 2,
                Athletics = 0,
                AnimalHandling= 0,
                Perception= 0,
                Nature= 0,
                Survival = 0,
                Intimidation = 0

            }
        };
    }
}