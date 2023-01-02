using OstreCWEB.Data.Repository.Characters.CoreClasses;
using OstreCWEB.Data.Repository.Characters.Enums;

namespace OstreCWEB.Data.Repository.Characters
{
    public class CharacterRepository
    {
        public List<PlayableCharacter> characters = new List<PlayableCharacter>
        {
            new PlayableCharacter
            {
                CharacterId = 1,
                CharacterName = "AdminCharacter",
                CurrentHealthPoints = 10,
                Level = 1,
                Alignment = "Good",                    
                //EquippedArmor = Items.First(c=>c.Id==1),
                //EquippedWeapon = Items.First(c=>c.Id==2),
                //EquippedSecondaryWeapon =Items.First(c =>c.Id ==4),
                //Inventory = new Item[5],
                //AllAvailableActions = new List<CharacterActions>(),
                Strength = 16,
                //ModStrength =2,
                Dexterity = 14,
                //ModDexterity=1,
                Constitution = 10,
                //ModConstitution=1,
                Intelligence = 15,
                //ModIntelligence =1,
                Wisdom = 12,
                //ModWisdom=1,
                Charisma = 2,
                //ModCharisma= 1,
                //Race = PlayableRaces.FirstOrDefault(r=>r.ID ==1),
                //UserId = Users.FirstOrDefault(u=>u.Id == 1).Id,
                //CharacterClass =PlayableCharacterClasses.FirstOrDefault(c=>c.ID ==1)

            }
        };



        public List<PlayableCharacterClass> characterClass = new List<PlayableCharacterClass>
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
    }
}
