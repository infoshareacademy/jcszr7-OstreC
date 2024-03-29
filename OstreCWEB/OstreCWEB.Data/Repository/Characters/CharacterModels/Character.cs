﻿using OstreCWEB.Data.Repository.Characters.MetaTags;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OstreCWEB.Data.Repository.Characters.CharacterModels
{
    public abstract class Character
    {
        //Ef Config
        //=============================================================//
        [Key]
        public int CharacterId { get; set; }
        //Actions granted on level 1 based on class+race+ user choices. 
        public List<ActionCharacter>? LinkedActions { get; set; }
        //All items ( equipped+ in inventory)
        public List<ItemCharacter>? LinkedItems { get; set; }
        public bool IsTemplate { get; set; }
        //=============================================================//
        [DisplayName("Character Name")]
        public string CharacterName { get; set; }
        public int MaxHealthPoints { get; set; }
        public int CurrentHealthPoints { get; set; }
        [DisplayName("Strength")]
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
        public List<CharacterAction>? InnateActions { get; set; }

        [NotMapped]
        public List<CharacterAction> AllAvailableActions
        {
            get
            {
                var allAvailableActions = new List<CharacterAction>();
                foreach (var item in EquippedItems) { if (item.ActionToTrigger != null) { allAvailableActions.Add(item.ActionToTrigger); } }
                foreach (var action in InnateActions) { if (action != null) { allAvailableActions.Add(action); } }
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
            InnateActions = new List<CharacterAction>(); 
            ActiveStatuses = new List<Status>();
            LinkedItems = new List<ItemCharacter>();
            LinkedActions = new List<ActionCharacter>();

        }
    }
}

