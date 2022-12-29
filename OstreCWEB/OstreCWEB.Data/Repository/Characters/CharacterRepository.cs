using OstreCWEB.Data.Repository.Characters.CoreClasses;

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

            }
        };
    }
}
