﻿using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.Repository.DataBase;

namespace OstreCWEB.Repository.InitialData
{
    internal class SeedCharacters
    {
        internal static void Seed(OstreCWebContext context, List<User> users)
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
            var actions = new List<Ability>
            {
                 new Ability
            {
                AbilityName = "1d6 attack",
                AbilityDescription = "Strikes the chosen character with your weapon",
                ActionType = AbilityType.WeaponAttack,
                SavingThrowPossible = false,
                Max_Dmg = 6,
                Flat_Dmg = 1,
                Hit_Dice_Nr = 1,
                PossibleTarget = TargetType.Target,
                InflictsStatus = false,
                StatForTest = Statistics.Strenght,
                AggressiveAction = true

            },
                          new Ability
            {
                AbilityName = "2d6 attack",
                AbilityDescription = "Strikes the chosen character with your weapon",
                ActionType = AbilityType.WeaponAttack,
                SavingThrowPossible = false,
                Max_Dmg = 6,
                Flat_Dmg = 2,
                Hit_Dice_Nr = 2,
                PossibleTarget = TargetType.Target,
                InflictsStatus = false,
                StatForTest = Statistics.Strenght,
                AggressiveAction = true

            },

                      new Ability
            {
                AbilityName = "Fireball",
                AbilityDescription = "Throws a fireball",
                ActionType = AbilityType.Cantrip,
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

                    new Ability
            {
                AbilityName = "Fist Attack",
                AbilityDescription = "Strikes the chosen character with your bare hands",
                ActionType = AbilityType.WeaponAttack,
                SavingThrowPossible = true,
                Max_Dmg = 2,
                Flat_Dmg = 1,
                Hit_Dice_Nr = 1,
                PossibleTarget = TargetType.Target,
                InflictsStatus = false,
                StatForTest = Statistics.Strenght,
                AggressiveAction = true
            },
                    new Ability
            {
                AbilityName = "Magic Missiles",
                AbilityDescription = "Throws magic missiles at the enmy",
                ActionType = AbilityType.Spell,
                SavingThrowPossible = true,
                Max_Dmg = 4,
                Flat_Dmg = 2,
                Hit_Dice_Nr = 2,
                PossibleTarget = TargetType.Target,
                InflictsStatus = true,
                StatForTest = Statistics.Dexterity,
                UsesMaxBeforeRest = 2,
                 AggressiveAction = true

            },
                              new Ability
            {
                AbilityName = "Small Heal",
                AbilityDescription = "Heals the user for 1d6 +2",
                SavingThrowPossible = false,
                ActionType = AbilityType.Spell,
                Max_Dmg = 6,
                Flat_Dmg = 2,
                Hit_Dice_Nr = 1,
                PossibleTarget = TargetType.Caster,
                InflictsStatus = false,
                StatForTest = Statistics.None,
                UsesMaxBeforeRest = 2,
                AggressiveAction = false
            },
                 new Ability
            {
                AbilityName = "Bless",
                AbilityDescription = "Blesses the target giving him advantage a bonus 1d4 to attack rolls",
                ActionType = AbilityType.Spell,
                     SavingThrowPossible = false,
                Max_Dmg = 0,
                Flat_Dmg = 0,
                Hit_Dice_Nr = 0,
                PossibleTarget = TargetType.Caster,
                InflictsStatus = true,
                StatForTest = Statistics.None,
                UsesMaxBeforeRest = 1,
                 AggressiveAction = false
            },

                 new Ability
            {
                AbilityName = "Action Surge",
                AbilityDescription = "Gives you one more action once per day and once per turn",
                ActionType = AbilityType.SpecialAction,
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
            context.CharacterActions.AddRange(actions);
            context.Statuses.AddRange(statuses);
            context.PlayableCharacterRaces.AddRange(playableRaces);
            context.PlayableCharacterClasses.AddRange(playableCharacterClasses);
            context.Items.AddRange(items);
            context.SaveChanges();
            //adding actions to items
            items.FirstOrDefault(i => i.Name.Contains("Two Handed Sword")).ActionToTrigger = actions.First(a => a.AbilityName.Contains("2d6 attack"));
            items.FirstOrDefault(i => i.Name.Contains("Short Sword")).ActionToTrigger = actions.FirstOrDefault(a => a.AbilityName.Contains("1d6 attack"));
            items.FirstOrDefault(i => i.Name.Contains("Healing Potion")).ActionToTrigger = actions.FirstOrDefault(a => a.AbilityName.Contains("Small Heal"));
            //adding statuses to actions
            actions.FirstOrDefault(a => a.AbilityName.Contains("Magic Missiles")).Status = statuses.FirstOrDefault(s => s.Name.Contains("Blind"));
            actions.FirstOrDefault(a => a.AbilityName.Contains("Bless")).Status = statuses.FirstOrDefault(s => s.Name.Contains("Bless"));
            //adding items to classes
            playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Wizard").ItemsGrantedByClass.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("mage robe")));

            playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Fighter").ItemsGrantedByClass.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("two handed sword")));
            playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Fighter").ItemsGrantedByClass.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("heavy armor")));
            playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Fighter").ItemsGrantedByClass.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("healing potion")));

            playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Cleric").ItemsGrantedByClass.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("short sword")));
            playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Cleric").ItemsGrantedByClass.Add(items.FirstOrDefault(i => i.Name.ToLower().Contains("small wooden shield")));
            //adding actions to classes 

            playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Wizard").ActionsGrantedByClass.Add(actions.FirstOrDefault(i => i.AbilityName.ToLower().Contains("magic missiles")));
            playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Wizard").ActionsGrantedByClass.Add(actions.FirstOrDefault(i => i.AbilityName.ToLower().Contains("fireball")));

            playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Cleric").ActionsGrantedByClass.Add(actions.FirstOrDefault(i => i.AbilityName.ToLower().Contains("small heal")));
            playableCharacterClasses.FirstOrDefault(c => c.ClassName == "Cleric").ActionsGrantedByClass.Add(actions.FirstOrDefault(i => i.AbilityName.ToLower().Contains("bless")));


            context.SaveChanges();
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
            context.Enemies.AddRange(enemies);

            users.FirstOrDefault(u => u.UserName == "AdminUser").CharactersCreated.Add(playableCharacters[0]);
            users[0].CharactersCreated.Add(playableCharacters[1]);
            users[1].CharactersCreated.Add(playableCharacters[2]);
            context.SaveChanges();


        }
        public static List<PlayableCharacter>  UpdatePlayableCharacterItemsRelations(List<PlayableCharacter> characters)
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
        public static List<Enemy> UpdateEnemyItemsRelations(List<Enemy> characters)
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
        public static List<PlayableCharacter> UpdatePlayableCharacterActionsRelations(List<PlayableCharacter> characters)
        {
            foreach (var character in characters)
            {
                //Deletes all many to many relations
                character.LinkedAbilities = new List<AbilitiesCharacter>();

                foreach (var action in character.InnateAbilities)
                {
                    //Creates a new relation object for each action. 
                    character.LinkedAbilities.Add(
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
        public static List<Enemy> UpdateEnemyActionsRelations(List<Enemy> characters)
        {
            foreach (var character in characters)
            {
                //Deletes all many to many relations
                character.LinkedAbilities = new List<AbilitiesCharacter>();

                foreach (var action in character.InnateAbilities)
                {
                    //Creates a new relation object for each action. 
                    character.LinkedAbilities.Add(
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
}