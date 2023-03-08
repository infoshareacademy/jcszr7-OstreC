using OstreCWEB.DomainModels.CharacterModels;
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
                StrengthBonus = 1,
                RaceDescription ="Humans are one of the most versatile and common races in DnD. They are known for their quick adaptability," +
                                 " their ability to learn new skills quickly, and their diverse cultural backgrounds. Humans are also known for their ambition and their drive to succeed," +
                                 " which makes them a popular choice for players who want to create a character with a wide range of possibilities. In game mechanics," +
                                 " Humans have an ability score bonus, which means that they receive a bonus to one or more of their ability scores," +
                                 " such as Strength, Dexterity, or Intelligence. They also have the ability to choose a bonus feat at 1st level," +
                                 " which allows them to gain a special ability or skill that sets them apart from other races. Overall," +
                                 " Humans are a good choice for players who want a versatile and adaptable character," +
                                 " or for those who want a character that is similar to themselves in terms of abilities and personality." +
                                 " They can fit into a wide range of play styles and can be effective in many different types of adventures."
            },
            new PlayableRace
            {
                RaceName = "Elf",
                ConstitutionBonus = 1,
                StrengthBonus = 1,
                RaceDescription = "Elves are a long-lived race known for their grace, beauty, and skill with magic. They are often depicted as being deeply connected to the natural world," +
                                  " and they live in forests, glades, and other sylvan settings. Elves have several unique traits and abilities that set them apart from other races." +
                                  " They are nimble and quick, with a bonus to their Dexterity score, and they have keen senses, making them excellent scouts and archers." +
                                  " Elves also have resistance to magic, which makes them more resistant to spells and other magical effects. In addition to their physical abilities," +
                                  " Elves also have a strong connection to magic. Many Elves are born with the ability to cast spells, and they have a natural affinity for magic that makes them powerful arcane spellcasters." +
                                  " They also have an innate understanding of the natural world, which allows them to move quickly and quietly through forests and other wilderness areas." +
                                  " Overall, Elves are a good choice for players who want a character that is graceful, quick, and capable of casting spells." +
                                  " They are ideal for players who enjoy playing characters that are deeply connected to the natural world, or for those who want a character that is highly skilled in archery or stealth." +
                                  " Elves can be adapted to a wide range of play styles, from cunning rangers to powerful wizards, and they are well-suited for many different types of adventures."
            },
            new PlayableRace
            {
                RaceName = "Dwarf",
                IntelligenceBonus = 1,
                WisdomBonus = 1,
                RaceDescription = "Dwarfs are a race in Dungeons and Dragons known for their skill in mining, metalworking, and their love of treasure." +
                                  " They are a sturdy and resilient people, with a bonus to their Constitution score, and they are known for their courage and determination." +
                                  " Dwarfs live in underground cities and mines, where they work tirelessly to extract valuable minerals and gems from the earth." +
                                  " They are skilled craftsmen, and they are known for their ability to create high-quality weapons and armor. They also have a strong sense of community," +
                                  " and they value their friends and family above all else. In combat, Dwarfs are known for their toughness and their ability to absorb damage." +
                                  " They are also skilled in the use of axes and hammers, which makes them formidable in close combat. They have the ability to move quickly through difficult terrain," +
                                  " such as mountains and caverns, and they can see in the dark, which makes them effective in underground environments." +
                                  " Overall, Dwarfs are a good choice for players who want a character that is tough, resilient, and capable of wielding powerful weapons." +
                                  " They are ideal for players who enjoy playing characters that are highly skilled in crafting and mining," +
                                  " or for those who want a character that is deeply connected to the earth and its treasures. Dwarfs can be adapted to a wide range of play styles," +
                                  " from brave warriors to cunning thieves, and they are well-suited for many different types of adventures."
            }
        };

            var playableCharacterClasses = new List<PlayableClass>
            {
                 new PlayableClass
                {
                    ClassName = "Fighter",
                    StrengthBonus=1,
                    ConstitutionBonus=1,
                    BaseHP = 20,
                    ClassDescription = "A fighter is a character class that focuses on combat prowess and physical prowess." +
                                       " Fighters are versatile characters that can wield a variety of weapons and armor and have the ability to adapt to different battle situations." +
                                       " They have a high hit point value and strong defense, making them formidable in close combat. They can also serve as the partys primary defender," +
                                       " using their combat skills and bravery to protect their allies. With a variety of fighting styles, weapons, and armor options, the fighter class is" +
                                       " ideal for players who enjoy engaging in direct combat and tactical battles."
                },
                 new PlayableClass
                {
                    ClassName = "Wizard",
                    IntelligenceBonus=1,
                    BaseHP = 15,
                    ClassDescription =  "The Wizard is a character class in DnD that specializes in casting spells. Wizards have a wide range of spells at their disposal," +
                                        " which they can use to attack their enemies, heal their allies, or manipulate the environment to their advantage." +
                                        " Wizards have access to a spell book that contains the spells they know and they can learn new spells as they gain experience." +
                                        " Wizards are known for their intelligence and their mastery of magic, but they are often physically weak and vulnerable in combat." +
                                        " To compensate for their lack of physical prowess, they rely on their spells and their ability to use them strategically in battle." +
                                        " Wizards are versatile characters who can adapt to a wide range of play styles and can be effective in many different types of adventures." +
                                        " Overall, the Wizard class is well-suited for players who enjoy casting spells, solving puzzles, and using their wits to overcome obstacles."
                },
                   new PlayableClass
                {
                    ClassName = "Cleric",
                    WisdomBonus=1,
                    BaseHP = 15,
                    ClassDescription = "Clerics are divine spell casters who draw their power from a deity or pantheon of gods. They are often seen as holy warriors or healers," +
                                       " and their spells focus on supporting their allies and defeating their enemies. Clerics have access to a wide range of spells," +
                                       " including those that can heal wounds, remove curses, or smite their enemies. They also have the ability to turn undead creatures," +
                                       " which makes them powerful allies in battles against the undead. Clerics are able to use heavy armor and weapons," +
                                       " which makes them more durable in combat than other spell casting classes. In addition to their spells," +
                                       " Clerics also have the ability to channel divine energy to fuel their class abilities. This allows them to perform powerful attacks or grant bonuses to their allies." +
                                       " Clerics are also capable of performing rituals, which can have a wide range of effects, such as creating magical portals or calling forth powerful spirits." +
                                       " Clerics are a good choice for players who enjoy playing supportive characters or who want a character that is capable of both casting spells and participating in combat." +
                                       " They are versatile and can be adapted to a wide range of play styles, from supporting their allies as healers to leading the charge as holy warriors."
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
                AbilityDescription = "Throws magic missiles at the enemy",
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
            items.FirstOrDefault(i => i.Name.Contains("Two Handed Sword")).Ability = actions.First(a => a.AbilityName.Contains("2d6 attack"));
            items.FirstOrDefault(i => i.Name.Contains("Short Sword")).Ability = actions.FirstOrDefault(a => a.AbilityName.Contains("1d6 attack"));
            items.FirstOrDefault(i => i.Name.Contains("Healing Potion")).Ability = actions.FirstOrDefault(a => a.AbilityName.Contains("Small Heal"));
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

        public static List<PlayableCharacter> UpdatePlayableCharacterItemsRelations(List<PlayableCharacter> characters)
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
                             CharacterId = character.Id,
                             ItemId = item.Id,
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
                                    CharacterId = character.Id,
                                    ItemId = item.Id,
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
                             CharacterId = character.Id,
                             ItemId = item.Id,
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
                                  CharacterId = character.Id,
                                  ItemId = item.Id,
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