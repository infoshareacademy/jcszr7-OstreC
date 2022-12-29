using OstreCWEB.Data.Repository.Characters.CharacterModels;
using OstreCWEB.Data.Repository.Characters.MetaTags;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OstreCWEB.Data.Repository.Characters.CoreClasses
{
    public abstract class Character
    {
        [Key]
        public int CharacterId { get; set; }
        //This is required because Characters IDs can be the same as enemies...Combat needs to generate it's own IDs
        //This is not saved to data base.
        public int CombatId { get; set; }

        [Required]
        public string CharacterName { get; set; }
        [Required]
        public int MaxHealthPoints { get; set; }
        public int CurrentHealthPoints { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public string Alignment { get; set; }
        [Required]
        [NotMapped]
        public Item EquippedArmor { get; set; } 
        [Required]
        [NotMapped]
        public Item EquippedWeapon { get; set; } 
        [Required]
        [NotMapped]
        public Item EquippedSecondaryWeapon { get; set; } 
        [NotMapped]
        public Item[] Inventory { get; set; }
        //Actions granted on level 1 based on class+race+ user choices.
        public List<CharacterAction> DefaultActions { get; set; }


        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        [NotMapped]
        public List<CharacterAction> AllAvailableActions { get; set; }
        [NotMapped]
        public List<Status> ActiveStatuses { get; set; }

        public List<ItemCharacter> ItemCharacter { get; set; }

        [JsonConstructor]
        public Character()
        {

        }
    }

    //public Character(int id,string characterName,string race,int healtPoints,int level,string alignment,Item equippedArmor,Item equippedWeapon,
    //    Item equippedSecondaryWeapon, Item[] items,int strenght,int modStrenght,int dexterity,int modDexterity,int constitution,int modConstitution,
    //    int intelligence,int modIntelligence,int wisdom,int modWisdom,int charisma,int modCharisma)

    //{
    //    ID = id;
    //    CharacterName = characterName;
    //    Race = race;
    //    HealthPoints = healtPoints;
    //    Level = level;
    //    Alignment = alignment;
    //    EquippedArmor = (Armor)equippedArmor;
    //    EquippedWeapon = (Weapon)equippedWeapon;
    //    EquippedSecondaryWeapon = equippedSecondaryWeapon;
    //    Inventory = items;
    //    Strenght = strenght;
    //    ModStrenght = modStrenght;
    //    Dexterity = dexterity;
    //    ModDexterity = modDexterity;
    //    Constitution = constitution;
    //    ModConstitution = modConstitution; 
    //    Intelligence = intelligence;
    //    ModIntelligence = modIntelligence;
    //    Wisdom = wisdom;
    //    ModWisdom = modWisdom;
    //    Charisma = charisma;
    //    ModCharisma = charisma;
    //}
}

