using OstreCWEB.DomainModels.ManyToMany;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OstreCWEB.DomainModels.CharacterModels
{
    public abstract class Character
    {
        //Ef Config
        //=============================================================//
        [Key]
        public int Id { get; set; }
        //Actions granted on level 1 based on class+race+ user choices. 
        public List<AbilitiesCharacter>? LinkedAbilities { get; set; }
        //All items ( equipped+ in inventory)
        public List<ItemCharacter>? LinkedItems { get; set; }
        public bool IsTemplate { get; set; }
        //=============================================================// 
        public string CharacterName { get; set; }
        public int MaxHealthPoints { get; set; }
        public int CurrentHealthPoints { get; set; }
        public int Strenght { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }


        //Initialised based on LinkedItems
        [NotMapped]
        public List<Item> EquippedItems { get; set; }
        [NotMapped]
        public List<Item> Inventory { get; set; }
        [NotMapped]
        //Initialised based on LinkedActions 
        public List<Ability>? InnateAbilities { get; set; }

        [NotMapped]
        public List<Ability> AllAbilities
        {
            get
            {
                var allAvailableActions = new List<Ability>();
                foreach (var item in EquippedItems) { if (item.ActionToTrigger != null) { allAvailableActions.Add(item.ActionToTrigger); } }
                foreach (var action in InnateAbilities) { if (action != null) { allAvailableActions.Add(action); } }
                foreach (var item in Inventory)
                {
                    if (item != null && item.ActionToTrigger != null)
                    {
                        allAvailableActions.Add(item.ActionToTrigger);
                    }
                }
                return allAvailableActions;
            }
        }
        [NotMapped]
        public List<Status> ActiveStatuses { get; set; }
        [NotMapped]
        public int CombatId { get; set; }

        [JsonConstructor]
        public Character()
        {
            Inventory = new List<Item>();
            EquippedItems = new List<Item>();
            InnateAbilities = new List<Ability>();
            ActiveStatuses = new List<Status>();
            LinkedItems = new List<ItemCharacter>();
            LinkedAbilities = new List<AbilitiesCharacter>();

        }
    }
}

