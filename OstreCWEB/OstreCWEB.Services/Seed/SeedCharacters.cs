using Microsoft.AspNetCore.Identity;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.InitialData;
using OstreCWEB.Repository.Repository.Identity;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.Services.Identity;


namespace OstreCWEB.Services.Seed;

internal class SeedCharacters : ISeeder
{
    private readonly OstreCWebContext _db;
    private readonly UserManager<User> _userManager;
    private readonly IUserAuthenticationService _auth;
    private readonly IIdentityRepository _identity;
    public SeedCharacters(UserManager<User> userManager, OstreCWebContext ostreCWebContext, IUserAuthenticationService auth, IIdentityRepository identity)
    {
        _db = ostreCWebContext;
        _userManager = userManager;
        _auth = auth;
        _identity = identity;
    }
    public async Task SeedCharactersDb()
    {
        if (_db.Users.Any()) { return; }
        await Seed();
    }
    private async Task Seed()
    {
        var statuses = new List<Status>
        {
            new Status
            {
                StatusType = StatusType.Blind,
                Name="Blind",
                Description = "Blinds the character making him less accurate"
            },
            new Status
            {
                StatusType = StatusType.Bless,
                Name="Bless",
                Description="Character is blessed. It has advantage to every hit roll"
            }
        };

        var playableRaces = new List<PlayableRace>
        {
            new PlayableRace
            {
                RaceName = "Human",
                CharismaBonus = 1,
                StrengthBonus = 1
            },
            new PlayableRace
            {
                RaceName = "Dwarf",
                ConstitutionBonus = 1,
                StrengthBonus = 1
            },
            new PlayableRace
            {
                RaceName = "Elf",
                IntelligenceBonus = 1,
                WisdomBonus = 1
            }
        };



        var playableCharacterClasses = new List<PlayableClass>
            {
                 new PlayableClass
                {
                    ClassName = "Fighter",
                    StrengthBonus=1,
                    ConstitutionBonus=1,
                    BaseHP = 20
                },
                 new PlayableClass
                {
                    ClassName = "Wizard",
                    IntelligenceBonus=1,
                    BaseHP = 15
                },
                   new PlayableClass
                {
                    ClassName = "Cleric",
                    WisdomBonus=1,
                    BaseHP = 15
                }
            };
        //Property StatusName is null if none is applied.
        var actions = new List<Abilities>
            {
                 new Abilities
            {
                ActionName = "1d6 attack",
                ActionDescription = "Strikes the chosen character with your weapon",
                ActionType = CharacterActionType.WeaponAttack,
                SavingThrowPossible = false,
                Max_Dmg = 6,
                Flat_Dmg = 1,
                Hit_Dice_Nr = 1,
                PossibleTarget = TargetType.Target,
                InflictsStatus = false,
                StatForTest = Statistics.Strenght,
                AggressiveAction = true

            },
                          new Abilities
            {
                ActionName = "2d6 attack",
                ActionDescription = "Strikes the chosen character with your weapon",
                ActionType = CharacterActionType.WeaponAttack,
                SavingThrowPossible = false,
                Max_Dmg = 6,
                Flat_Dmg = 2,
                Hit_Dice_Nr = 2,
                PossibleTarget = TargetType.Target,
                InflictsStatus = false,
                StatForTest = Statistics.Strenght,
                AggressiveAction = true

            },

                      new Abilities
            {
                ActionName = "Fireball",
                ActionDescription = "Throws a fireball",
                ActionType = CharacterActionType.Cantrip,
                SavingThrowPossible = true,
                Max_Dmg = 8,
                Flat_Dmg = 2,
                Hit_Dice_Nr = 4,
                PossibleTarget = TargetType.Target,
                InflictsStatus = false,
                StatForTest = Statistics.Intelligence,
                AggressiveAction = true,
                 UsesMaxBeforeRest = 0

            },

                    new Abilities
            {
                ActionName = "Fist Attack",
                ActionDescription = "Strikes the chosen character with your bare hands",
                ActionType = CharacterActionType.WeaponAttack,
                SavingThrowPossible = true,
                Max_Dmg = 2,
                Flat_Dmg = 1,
                Hit_Dice_Nr = 1,
                PossibleTarget = TargetType.Target,
                InflictsStatus = false,
                StatForTest = Statistics.Strenght,
                AggressiveAction = true
            },
                    new Abilities
            {
                ActionName = "Magic Missiles",
                ActionDescription = "Throws magic missiles at the enmy",
                ActionType = CharacterActionType.Spell,
                SavingThrowPossible = true,
                Max_Dmg = 4,
                Flat_Dmg = 2,
                Hit_Dice_Nr = 2,
                PossibleTarget = TargetType.Target,
                InflictsStatus = true,
                Status = _db.Statuses.FirstOrDefault(s=>s.StatusId == 1),
                StatForTest = Statistics.Dexterity,
                UsesMaxBeforeRest = 2,
                 AggressiveAction = true

            },
                              new Abilities
            {
                ActionName = "Small Heal",
                ActionDescription = "Heals the user for 1d6 +2",
                SavingThrowPossible = false,
                ActionType = CharacterActionType.Spell,
                Max_Dmg = 6,
                Flat_Dmg = 2,
                Hit_Dice_Nr = 1,
                PossibleTarget = TargetType.Caster,
                InflictsStatus = false,
                StatForTest = Statistics.None,
                UsesMaxBeforeRest = 2,
                AggressiveAction = false
            },
                 new Abilities
            {
                ActionName = "Bless",
                ActionDescription = "Blesses the target giving him advantage a bonus 1d4 to attack rolls",
                ActionType = CharacterActionType.Spell,
                     SavingThrowPossible = false,
                Max_Dmg = 0,
                Flat_Dmg = 0,
                Hit_Dice_Nr = 0,
                PossibleTarget = TargetType.Caster,
                InflictsStatus = true,
                Status = _db.Statuses.FirstOrDefault(s=>s.StatusId == 2),
                StatForTest = Statistics.None,
                UsesMaxBeforeRest = 1,
                 AggressiveAction = false
            },

                 new Abilities
            {
                ActionName = "Action Surge",
                ActionDescription = "Gives you one more action once per day and once per turn",
                ActionType = CharacterActionType.SpecialAction,
                     SavingThrowPossible = false,
                Max_Dmg = 0,
                Flat_Dmg = 0,
                Hit_Dice_Nr = 0,
                PossibleTarget = TargetType.Caster,
                InflictsStatus = false,
                StatForTest = Statistics.None,
                UsesMaxBeforeRest = 1,
                 AggressiveAction = false
            }
        };
        var items = new List<Item>
            {
                new Item()
                {
                    Name="Short Sword",
                    ItemType =ItemType.SingleHandedWeapon,
                    DeleteOnUse = false
                },
                 new Item()
                {
                    Name="Two Handed Sword",
                    ItemType =ItemType.TwoHandedWeapon,
                    DeleteOnUse = false
                },
                new Item()
                {
                    Name="Healing Potion",
                    ItemType = ItemType.SpecialItem,
                    DeleteOnUse = true

                },
                new Item()
                {
                    Name="Small Wooden Shield",
                    ItemType = ItemType.Shield,
                    ArmorClass=2,
                    DeleteOnUse = false
                },
                 new Item
                {
                    Name="Heavy Armor",
                    ItemType = ItemType.Armor,
                    ArmorClass = 16,
                    DeleteOnUse=false
                },
                new Item
                {
                    Name="Mage Robe",
                    ItemType = ItemType.Armor,
                    ArmorClass = 10,
                    DeleteOnUse = false
                }
            };
        var enemies = new List<Enemy>
            {
                new Enemy
                {
                    CharacterName = "Goblin Archer",
                    NonPlayableRace = Races.Humanoid,
                    MaxHealthPoints = 8,
                    CurrentHealthPoints = 8,
                    Strenght = 10,
                    Dexterity =12,
                    Constitution=10,
                    Intelligence=8,
                    Wisdom = 8,
                    Charisma = 6,
                     IsTemplate= true
                },
                  new Enemy
                {
                    CharacterName = "Orc",
                    NonPlayableRace = Races.Humanoid,
                    MaxHealthPoints = 15,
                    CurrentHealthPoints = 15,
                    Strenght = 15,
                    Dexterity =12,
                    Constitution=15,
                    Intelligence=10,
                    Wisdom = 8,
                    Charisma = 6,
                     IsTemplate= true
                }
            };
        var playableCharacters = new List<PlayableCharacter>
            {
                new PlayableCharacter
                {
                    CharacterName = "AdminCharacterWarrior",
                    MaxHealthPoints = 30,
                    CurrentHealthPoints = 30,
                    Strenght = 16,
                    Dexterity = 14,
                    Constitution = 10,
                    Intelligence = 15,
                    Wisdom = 12,
                    Charisma = 2,
                    IsTemplate= true
                },
                new PlayableCharacter
                {
                    CharacterName = "AdminCharacterMage",
                    MaxHealthPoints = 30,
                    CurrentHealthPoints = 30,
                    Strenght = 10,
                    Dexterity = 10,
                    Constitution = 14,
                    Intelligence = 18,
                    Wisdom = 14,
                    Charisma = 12,
                    IsTemplate = true
                },
                 new PlayableCharacter
                {
                    CharacterName = "AdminCharacterCleric",
                    MaxHealthPoints = 30,
                    CurrentHealthPoints = 30,
                    Strenght = 16,
                    Dexterity = 10,
                    Constitution = 14,
                    Intelligence = 16,
                    Wisdom = 12,
                    Charisma = 12,
                    IsTemplate = true
                }
            };

        var register = new Registration();
        var password = "";

        for (var i = 1; i < 10; i++)
        {
            password = "NewPassword@" + i;
            register = new Registration
            {
                Name = "NewUser" + i,
                Email = $"Test{i}@test.com",
                UserName = "NewUserName" + i,
                Password = password,
                PasswordConfirm = password,
                Role = "user"

            };
            await _auth.RegisterAsync(register);
        }
        password = "Admin123#";
        register = new Registration
        {

            Name = "Admin",
            Email = $"Test@test.com",
            UserName = "AdminUser",
            Password = password,
            PasswordConfirm = password,
            Role = "admin"

        };
        await _auth.RegisterAsync(register);
        var users = await _identity.GetAll();
        _db.CharacterActions.AddRange(actions);
        _db.Statuses.AddRange(statuses);
        _db.PlayableCharacterRaces.AddRange(playableRaces);
        _db.PlayableCharacterClasses.AddRange(playableCharacterClasses);
        _db.Items.AddRange(items);
        _db.SaveChanges();
        //adding actions to items
        items.FirstOrDefault(i => i.Name.Contains("Two Handed Sword")).ActionToTrigger = actions.First(a => a.ActionName.Contains("2d6 attack"));
        items.FirstOrDefault(i => i.Name.Contains("Short Sword")).ActionToTrigger = actions.FirstOrDefault(a => a.ActionName.Contains("1d6 attack"));
        items.FirstOrDefault(i => i.Name.Contains("Healing Potion")).ActionToTrigger = actions.FirstOrDefault(a => a.ActionName.Contains("Small Heal"));
        //adding statuses to actions
        actions.FirstOrDefault(a => a.ActionName.Contains("Magic Missiles")).Status = statuses.FirstOrDefault(s => s.Name.Contains("Blind"));
        actions.FirstOrDefault(a => a.ActionName.Contains("Bless")).Status = statuses.FirstOrDefault(s => s.Name.Contains("Bless"));
        //adding items to classes
        playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Wizard").ItemsGrantedByClass.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("mage robe")));

        playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Fighter").ItemsGrantedByClass.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("two handed sword")));
        playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Fighter").ItemsGrantedByClass.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("heavy armor")));
        playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Fighter").ItemsGrantedByClass.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("healing potion")));

        playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Cleric").ItemsGrantedByClass.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("short sword")));
        playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Cleric").ItemsGrantedByClass.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("small wooden shield")));
        //adding actions to classes 

        playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Wizard").ActionsGrantedByClass.Add(actions.FirstOrDefault(i => i.ActionName.ToLower().Contains("magic missiles")));
        playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Wizard").ActionsGrantedByClass.Add(actions.FirstOrDefault(i => i.ActionName.ToLower().Contains("fireball")));

        playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Cleric").ActionsGrantedByClass.Add(actions.FirstOrDefault(i => i.ActionName.ToLower().Contains("small heal")));
        playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Cleric").ActionsGrantedByClass.Add(actions.FirstOrDefault(i => i.ActionName.ToLower().Contains("bless")));


        _db.SaveChanges();
        foreach (var enemy in enemies)
        {
            enemy.EquippedItems.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("armor")));
            enemy.EquippedItems.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("short sword")));
            enemy.EquippedItems.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("small wooden shield")));
        }

        //warrior  
        playableCharacters[0].Race = playableRaces.FirstOrDefault(i => i.RaceName.ToLower().Contains("human"));
        playableCharacters[0].CharacterClass = playableCharacterClasses.FirstOrDefault(i => i.ClassName.ToLower().Contains("fighter"));
        //mage  
        playableCharacters[1].Race = playableRaces.FirstOrDefault(i => i.RaceName.ToLower().Contains("human"));
        playableCharacters[1].CharacterClass = playableCharacterClasses.FirstOrDefault(i => i.ClassName.ToLower().Contains("wizard"));
        //cleric
        playableCharacters[2].Race = playableRaces.FirstOrDefault(i => i.RaceName.ToLower().Contains("human"));
        playableCharacters[2].CharacterClass = playableCharacterClasses.FirstOrDefault(i => i.ClassName.ToLower().Contains("cleric"));


        enemies = UpdateEnemyItemsRelations(enemies);
        enemies = UpdateEnemyActionsRelations(enemies);
        //playableCharacters = UpdatePlayableCharacterActionsRelations(playableCharacters); 
        //playableCharacters = UpdatePlayableCharacterItemsRelations(playableCharacters); 
        _db.Enemies.AddRange(enemies);

        users.FirstOrDefault(u => u.UserName == "AdminUser").CharactersCreated.Add(playableCharacters[0]);
        users[1].CharactersCreated.Add(playableCharacters[1]);
        users[2].CharactersCreated.Add(playableCharacters[2]);
        _db.SaveChanges();
        SeedStories.Initialize(_db, users.FirstOrDefault(u => u.UserName == "AdminUser"));
    }

    public List<PlayableCharacter> UpdatePlayableCharacterItemsRelations(List<PlayableCharacter> characters)
    {
        foreach (var character in characters)
        {
            //Deletes all many to many relations
            character.LinkedItems = new List<ItemCharacter>();
            if (character.EquippedItems.Any())
            {
                foreach (var item in character.EquippedItems)
                {
                    //Creates a new relation object for each action. 
                    character.LinkedItems.Add(
                     new ItemCharacter()
                     {
                         CharacterId = character.CharacterId,
                         ItemId = item.ItemId,
                         IsEquipped = true
                     });
                }
            }

            if (character.Inventory.Any())
            {

                foreach (var item in character.Inventory)
                {
                    if (item != null)
                    {
                        character.LinkedItems.Add(
                            new ItemCharacter()
                            {
                                CharacterId = character.CharacterId,
                                ItemId = item.ItemId,
                                IsEquipped = false
                            });
                    }
                }
            }
        };
        return characters;
    }
    public List<Enemy> UpdateEnemyItemsRelations(List<Enemy> characters)
    {
        foreach (var character in characters)
        {
            //Deletes all many to many relations
            character.LinkedItems = new List<ItemCharacter>();
            if (character.EquippedItems.Any())
            {
                foreach (var item in character.EquippedItems)
                {
                    //Creates a new relation object for each action. 
                    character.LinkedItems.Add(
                     new ItemCharacter()
                     {
                         CharacterId = character.CharacterId,
                         ItemId = item.ItemId,
                         IsEquipped = true
                     });
                }
            }
            if (character.Inventory.Any())
            {
                foreach (var item in character.Inventory)
                {
                    if (item != null)
                    {
                        character.LinkedItems.Add(
                          new ItemCharacter()
                          {
                              CharacterId = character.CharacterId,
                              ItemId = item.ItemId,
                              IsEquipped = true
                          });
                    }
                }
            }
        };
        return characters;
    }
    public List<PlayableCharacter> UpdatePlayableCharacterActionsRelations(List<PlayableCharacter> characters)
    {
        foreach (var character in characters)
        {
            //Deletes all many to many relations
            character.LinkedActions = new List<AbilitiesCharacter>();

            foreach (var action in character.InnateActions)
            {
                //Creates a new relation object for each action. 
                character.LinkedActions.Add(
                 new AbilitiesCharacter()
                 {
                     Character = character,
                     CharacterAction = action,
                     UsesLeftBeforeRest = action.UsesMaxBeforeRest
                 });
            }
        };
        return characters;
    }
    public List<Enemy> UpdateEnemyActionsRelations(List<Enemy> characters)
    {
        foreach (var character in characters)
        {
            //Deletes all many to many relations
            character.LinkedActions = new List<AbilitiesCharacter>();

            foreach (var action in character.InnateActions)
            {
                //Creates a new relation object for each action. 
                character.LinkedActions.Add(
                 new AbilitiesCharacter()
                 {
                     Character = character,
                     CharacterAction = action,
                     UsesLeftBeforeRest = action.UsesMaxBeforeRest
                 });
            }
        };
        return characters;
    }
}

