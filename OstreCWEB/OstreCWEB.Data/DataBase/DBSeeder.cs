using OstreCWEB.Data.Repository.Identity;

namespace OstreCWEB.Data.DataBase
{
    public class DBSeeder : ISeeder
    { 
        private readonly OstreCWebContext _db;

         public DBSeeder(OstreCWebContext ostreCWebContext)
        {
            _db = ostreCWebContext;
        }

        public void SeedDataBase()
        {
            Seed();
        }

        private void Seed()
        {
            //Statuses = new List<Status>
            //{
            //    new Status
            //    {
            //        ID = 1,
            //        Name="Blind",
            //        Description = "Blinds the character making him less accurate"

            //    },
            //    new Status
            //    {
            //        ID=2,
            //        Name="Bless",
            //        Description="Character is blessed. It has a bonus 1d4 to every roll"
            //    }
            //};

            //PlayableRaces = new List<PlayableRace>
            //{
            //    new PlayableRace
            //    {
            //        ID = 1,
            //        RaceName = "Human",
            //        DefaultSkillsForClass = new List<Skill>
            //        {
            //                Skill.acrobatics,
            //                Skill.religion
            //            },
            //        AmountOfSkillsToChoose = 1
            //    }

            //};

           
             
                _db.Users.AddRange(new List<User>
                {
                     
           
                new User
                { 
                    LoggedIn = false,
                    UserName = "Admin",
                    Password = "Admin",
                    Email = "Admin@Admin.com",
                    Created = DateTime.Now, 
                    StoriesCompletedTotal = 0,
                    DamageDealt = 0,
                    DamageReceived = 0
                }
            });



            //    PlayableCharacterClasses = new List<PlayableCharacterClass>
            //    {
            //        new PlayableCharacterClass
            //        {
            //            ID = 1,
            //            ClassName = "Warrior",
            //            //BonusesForEeachStatistic = new Dictionary<Statistics, int>
            //            //{
            //            //    {Statistics.Strenght,1},
            //            //    {Statistics.Dexterity,1}
            //            //}
            //        }
            //    };


            //    //Property StatusName is null if none is applied.
            //    Actions = new List<CharacterActions>
            //    {
            //         new CharacterActions
            //    {
            //        CharacterId = 1,
            //        ActionName = "Short Sword Attack",
            //        ActionDescription = "Strikes the chosen character with your weapon",
            //        ActionType = CharacterActionType.MeleeAttack,
            //        SavingThrowPossible = true,
            //        Max_Dmg = 6,
            //        Flat_Dmg = 1,
            //        Hit_Dice_Nr = 1,
            //        PossibleTargets = "enemy",
            //        InflictsStatus = false,
            //        StatForTest = Statistics.Strenght,
            //        AggressiveAction = true
            //    },
            //            new CharacterActions
            //    {
            //        CharacterId = 2,
            //        ActionName = "Fist Attack",
            //        ActionDescription = "Strikes the chosen character with your bare hands",
            //        ActionType = CharacterActionType.MeleeAttack,
            //        SavingThrowPossible = true,
            //        Max_Dmg = 2,
            //        Flat_Dmg = 1,
            //        Hit_Dice_Nr = 1,
            //        PossibleTargets = "enemy",
            //        InflictsStatus = false,
            //        StatForTest = Statistics.Strenght,
            //        AggressiveAction = true
            //    },
            //            new CharacterActions
            //    {
            //        CharacterId = 3,
            //        ActionName = "Magic Missiles",
            //        ActionDescription = "Throws magic missiles at the enmy",
            //        ActionType = CharacterActionType.MeleeAttack,
            //                SavingThrowPossible = true,
            //        Max_Dmg = 4,
            //        Flat_Dmg = 2,
            //        Hit_Dice_Nr = 2,
            //        PossibleTargets = "enemy",
            //        InflictsStatus = true,
            //        Status = Statuses.FirstOrDefault(s=>s.ID == 1),
            //        StatForTest = Statistics.Dexterity,
            //        UsesMaxBeforeRest = 2,
            //         AggressiveAction = true

            //    },
            //                      new CharacterActions
            //    {
            //        CharacterId = 4,
            //        ActionName = "Healing Potion",
            //        ActionDescription = "Heals the user for 1d6 +2",
            //        ActionType = CharacterActionType.ItemAction,
            //        SavingThrowPossible = false,
            //        Max_Dmg = 1,
            //        Flat_Dmg = 2,
            //        Hit_Dice_Nr = 1,
            //        PossibleTargets = "caster",
            //        InflictsStatus = false,
            //        StatForTest = Statistics.None,
            //        UsesMax = 1,
            //        AggressiveAction = false
            //    },
            //         new CharacterActions
            //    {
            //        CharacterId = 5,
            //        ActionName = "Bless",
            //        ActionDescription = "Blesses the target giving him advantage a bonus 1d4 to attack rolls",
            //        ActionType = CharacterActionType.Spell,
            //             SavingThrowPossible = false,
            //        Max_Dmg = 0,
            //        Flat_Dmg = 0,
            //        Hit_Dice_Nr = 0,
            //        PossibleTargets = "caster",
            //        InflictsStatus = true,
            //        Status = Statuses.FirstOrDefault(s=>s.ID==2),
            //        StatForTest = Statistics.None,
            //        UsesMaxBeforeRest = 1,
            //         AggressiveAction = false
            //    }

            //};

            //    Items = new List<Item>
            //    {
            //        new Item
            //        {
            //            CharacterId=1,
            //            Name="Light Armor",
            //            ItemType = ItemType.Armors,
            //            ArmorClass = 1,
            //            ArmorType="Light"

            //        },
            //        new Item()
            //        {
            //            CharacterId=2,
            //            Name="Short Sword",
            //            ItemType =ItemType.Weapons,
            //            ActionToTrigger = Actions.FirstOrDefault(a=> a.CharacterId == 1)
            //        },
            //        new Item()
            //        {
            //            CharacterId=3,
            //            Name="Healing Potion",
            //            ItemType = ItemType.Consumable,
            //            ActionToTrigger = Actions.FirstOrDefault(a=>a.CharacterId == 4 )
            //        },
            //        new Item()
            //        {
            //            CharacterId=4,
            //            Name="Small Wooden Shield",
            //            ItemType = ItemType.Shield,
            //            ArmorClass=1,
            //            ArmorType = "basic"

            //        }
            //    };

            //    Enemies = new List<Enemy>
            //    {
            //        new Enemy
            //        {
            //            ID = 1,
            //            CharacterName = "Goblin Archer",
            //            Race = "Goblin",
            //            MaxHealthPoints = 10,
            //            CurrentHealthPoints = 10,
            //            Level = 1,
            //            Alignment = "evil",
            //            //EquippedArmor = Items.FirstOrDefault(a => a.CharacterId == 1),
            //            //EquippedWeapon =Items.FirstOrDefault(a => a.CharacterId == 2),
            //            //EquippedSecondaryWeapon = Items.FirstOrDefault(a=>a.CharacterId == 4),
            //            //Inventory = new Item[5],
            //            //AllAvailableActions = new List<CharacterActions>(),
            //            //ActiveStatuses = new List<Status>(),
            //            Strenght = 10,
            //            Dexterity =12,
            //            Constitution=10,
            //            Intelligence=8,
            //            Wisdom = 8,
            //            Charisma = 6
            //        }
            //    };

            //    PlayableCharacters = new List<PlayableCharacter>
            //    {
            //        new PlayableCharacter
            //        {
            //            ID = 1,
            //            CharacterName = "AdminCharacter",
            //            MaxHealthPoints = 30,
            //            CurrentHealthPoints = 30,
            //            Level = 1,
            //            Alignment = "Good",
            //            //EquippedArmor = Items.First(c=>c.CharacterId==1),
            //            //EquippedWeapon = Items.First(c=>c.CharacterId==2),
            //            //EquippedSecondaryWeapon =Items.First(c =>c.CharacterId ==4),
            //            //Inventory = new Item[5],
            //            //AllAvailableActions = new List<CharacterActions>(),
            //            //DefaultActions = new List<CharacterActions>(),
            //            Strenght = 16,
            //            Dexterity = 14,
            //            Constitution = 10,
            //            Intelligence = 15,
            //            Wisdom = 12,
            //            Charisma = 2,
            //            Race = PlayableRaces.FirstOrDefault(r=>r.ID ==1),
            //            UserId = Users.FirstOrDefault(u=>u.CharacterId == 1).CharacterId,
            //            CharacterClass =PlayableCharacterClasses.FirstOrDefault(c=>c.ID ==1)

            //        }
            //    };
            //    //PlayableCharacters[0].DefaultActions.Add(Actions.FirstOrDefault(x => x.CharacterId == 4));  //HardCoding
            //    //PlayableCharacters[0].DefaultActions.Add(Actions.FirstOrDefault(x => x.CharacterId == 3));

            //}


            _db.SaveChanges();
        }
    }
}
