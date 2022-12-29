using OstreCWEB.Data.Repository.Characters.CharacterModels;
using OstreCWEB.Data.Repository.Characters.CoreClasses;
using OstreCWEB.Data.Repository.Characters.Enums;
using OstreCWEB.Data.Repository.Identity;
using OstreCWEB.Data.Repository.StoryModels;

namespace OstreCWEB.Data.DataBase
{
    public class StaticLists
    {
        private static List<User> Users = new List<User>(); 
        private static List<Enemy> Enemies = new List<Enemy>();
        private static List<PlayableCharacter> PlayableCharacters = new List<PlayableCharacter>();
        private static List<PlayableCharacterClass> PlayableCharacterClasses = new List<PlayableCharacterClass>();
        private static List<Status> Statuses = new List<Status>();
        private static List<PlayableRace> PlayableRaces = new List<PlayableRace>(); 
        private static List<Item> Items = new List<Item>();
        private static List<Repository.Characters.CoreClasses.CharacterAction> Actions = new List<Repository.Characters.CoreClasses.CharacterAction>();

        public PlayableCharacter GetPlayableCharacter(int id)
        {
            return PlayableCharacters.FirstOrDefault(x => x.CharacterId == id);
        }
        public StaticLists()
        {
        }

        public void SeedData()
        {
            Statuses = new List<Status>
            {
                new Status
                {
                    StatusId = 1,
                    Name="Blind",
                    Description = "Blinds the character making him less accurate"

                },
                new Status
                {
                    StatusId=2,
                    Name="Bless",
                    Description="Character is blessed. It has a bonus 1d4 to every roll"
                }
            };

            PlayableRaces = new List<PlayableRace>
            {
                new PlayableRace
                {
                    PlayableRaceId = 1,
                    RaceName = "Human",
                    DefaultSkillsForClass = new List<Skill>
                        {
                            Skill.acrobatics,
                            Skill.religion
                        },
                    AmountOfSkillsToChoose = 1
                }
                
            };

            Users = new List<User>
            {
                new User
                {
                    Id = 1,
                    LoggedIn = false,
                    UserName = "Admin",
                    Password = "Admin",
                    Email = "Admin@Admin.com",
                    Created = DateTime.Now,
                    //It's set to null but we will hold our active character instance here. 
                    //It should be chosen each session. 
                    ActiveCharacter = null,
                    //StoriesCreated = new List<Story>(),
                    CharactersCreated = new List<PlayableCharacter>(),
                    StoriesCompletedTotal = 0,
                    DamageDealt = 0,
                    DamageReceived = 0 
                }
            };

            PlayableCharacterClasses = new List<PlayableCharacterClass>
            {
                new PlayableCharacterClass
                {
                    ID = 1,
                    ClassName = "Warrior",
                    BonusesForEeachStatistic = new Dictionary<Statistics, int>
                    {
                        {Statistics.Strenght,1},
                        {Statistics.Dexterity,1}
                    }
                }
            };


            //Property StatusName is null if none is applied.
            Actions = new List<Repository.Characters.CoreClasses.CharacterAction>
            {
                 new Repository.Characters.CoreClasses.CharacterAction
            {
                CharacterActionId = 1,
                ActionName = "Short Sword Attack",
                ActionDescription = "Strikes the chosen character with your weapon",
                ActionType = CharacterActionType.MeleeAttack,
                SavingThrowPossible = true,
                Max_Dmg = 6,
                Flat_Dmg = 1,
                Hit_Dice_Nr = 1,
                PossibleTargets = "enemy",
                InflictsStatus = false,
                StatForTest = Statistics.Strenght,
                AggressiveAction = true
            },
                    new Repository.Characters.CoreClasses.CharacterAction
            {
                CharacterActionId = 2,
                ActionName = "Fist Attack",
                ActionDescription = "Strikes the chosen character with your bare hands",
                ActionType = CharacterActionType.MeleeAttack,
                SavingThrowPossible = true,
                Max_Dmg = 2,
                Flat_Dmg = 1,
                Hit_Dice_Nr = 1,
                PossibleTargets = "enemy",
                InflictsStatus = false,
                StatForTest = Statistics.Strenght,
                AggressiveAction = true
            },
                    new Repository.Characters.CoreClasses.CharacterAction
            {
                CharacterActionId = 3,
                ActionName = "Magic Missiles",
                ActionDescription = "Throws magic missiles at the enmy",
                ActionType = CharacterActionType.MeleeAttack,
                SavingThrowPossible = true,
                Max_Dmg = 4,
                Flat_Dmg = 2,
                Hit_Dice_Nr = 2,
                PossibleTargets = "enemy",
                InflictsStatus = true,
                Status = Statuses.FirstOrDefault(s=>s.StatusId == 1),
                StatForTest = Statistics.Dexterity,
                UsesMaxBeforeRest = 2,
                 AggressiveAction = true

            },
                              new Repository.Characters.CoreClasses.CharacterAction
            {
                CharacterActionId = 4,
                ActionName = "Healing Potion",
                ActionDescription = "Heals the user for 1d6 +2",
                ActionType = CharacterActionType.ItemAction,
                SavingThrowPossible = false,
                Max_Dmg = 1,
                Flat_Dmg = 2,
                Hit_Dice_Nr = 1,
                PossibleTargets = "caster",
                InflictsStatus = false,
                StatForTest = Statistics.None,
                UsesMax = 1,
                AggressiveAction = false
            },
                 new Repository.Characters.CoreClasses.CharacterAction
            {
                CharacterActionId = 5,
                ActionName = "Bless",
                ActionDescription = "Blesses the target giving him advantage a bonus 1d4 to attack rolls",
                ActionType = CharacterActionType.Spell,
                SavingThrowPossible = false,
                Max_Dmg = 0,
                Flat_Dmg = 0,
                Hit_Dice_Nr = 0,
                PossibleTargets = "caster",
                InflictsStatus = true,
                Status = Statuses.FirstOrDefault(s=>s.StatusId==2),
                StatForTest = Statistics.None,
                UsesMaxBeforeRest = 1,
                 AggressiveAction = false
            }

        };

            Items = new List<Item>
            {
                new Item
                {
                    ItemId=1,
                    Name="Light Armor",
                    ItemType = ItemType.Armors,
                    ArmorClass = 1,
                    ArmorType="Light"

                },
                new Item()
                {
                    ItemId=2,
                    Name="Short Sword",
                    ItemType =ItemType.Weapons,
                    ActionToTrigger = Actions.FirstOrDefault(a=> a.CharacterActionId == 1)
                },
                new Item()
                {
                    ItemId=3,
                    Name="Healing Potion",
                    ItemType = ItemType.Consumable,
                    ActionToTrigger = Actions.FirstOrDefault(a=>a.CharacterActionId == 4 )
                },
                new Item()
                {
                    ItemId=4,
                    Name="Small Wooden Shield",
                    ItemType = ItemType.Shield,
                    ArmorClass=1,
                    ArmorType = "basic"
                    
                } 
            };

            Enemies = new List<Enemy>
            {
                new Enemy
                {
                    CharacterId = 1,
                    CharacterName = "Goblin Archer",
                    Race = "Goblin",
                    MaxHealthPoints = 10,
                    CurrentHealthPoints = 10,
                    Level = 1,
                    Alignment = "evil",
                    EquippedArmor = Items.FirstOrDefault(a => a.ItemId == 1),
                    EquippedWeapon =Items.FirstOrDefault(a => a.ItemId == 2),
                    EquippedSecondaryWeapon = Items.FirstOrDefault(a=>a.ItemId == 4),
                    Inventory = new Item[5],
                    AllAvailableActions = new List<Repository.Characters.CoreClasses.CharacterAction>(),
                    ActiveStatuses = new List<Status>(),
                    Strength = 10, 
                    Dexterity =12, 
                    Constitution=10, 
                    Intelligence=8, 
                    Wisdom = 8, 
                    Charisma = 6, 
                }
            };

            PlayableCharacters = new List<PlayableCharacter>
            {
                new PlayableCharacter
                {
                    CharacterId = 1,
                    CharacterName = "AdminCharacter",
                    MaxHealthPoints = 30,
                    CurrentHealthPoints = 30,
                    Level = 1,
                    Alignment = "Good",
                    EquippedArmor = Items.First(c=>c.ItemId==1),
                    EquippedWeapon = Items.First(c=>c.ItemId==2),
                    EquippedSecondaryWeapon =Items.First(c =>c.ItemId ==4),
                    Inventory = new Item[5],
                    AllAvailableActions = new List<Repository.Characters.CoreClasses.CharacterAction>(),
                    DefaultActions = new List<Repository.Characters.CoreClasses.CharacterAction>(),
                    Strength = 16,
                    Dexterity = 14,
                    Constitution = 10,
                    Intelligence = 15,
                    Wisdom = 12, 
                    Charisma = 2,
                    Race = PlayableRaces.FirstOrDefault(r=>r.PlayableRaceId ==1),
                    UserId = Users.FirstOrDefault(u=>u.Id == 1).Id,
                    CharacterClass =PlayableCharacterClasses.FirstOrDefault(c=>c.ID ==1),
                   

                }
            };
            PlayableCharacters[0].DefaultActions.Add(Actions.FirstOrDefault(x => x.CharacterActionId == 4));  //HardCoding
            PlayableCharacters[0].DefaultActions.Add(Actions.FirstOrDefault(x => x.CharacterActionId == 3));

        }

        public Enemy GetEnemy()
        {
            return Enemies[0];
        }
        public List<Enemy> GetEnemies()
        {
            return Enemies;
        }
        public List<Item> GetItems()
         {
                return Items;
          }
           
            public List<Repository.Characters.CoreClasses.CharacterAction> GetActions()
            {
                return Actions;
            }
        };
    }

 