﻿using OstreCWEB.Data.Repository.Characters.CharacterModels;
using OstreCWEB.Data.Repository.Characters.Enums;
using System.ComponentModel;

namespace OstreCWEB.ViewModel.Characters
{
    public class CharacterView
    {
        public int CombatId { get; set; }
        [DisplayName("Character name")] 
        public string CharacterName { get; set; }
        [DisplayName("Current health points")]
        public int CurrentHealthPoints { get; set; }

        [DisplayName("Max health points")]
        public int MaxHealthPoints { get; set; }
        [DisplayName("Equipped Items")]
        public List<ItemView> EquippedItems { get; set; }

        [DisplayName("Inventory")]
        public List<ItemView> Inventory { get; set; }

        [DisplayName("Your abilities")]
        public List<CharacterActionView>? InnateActions { get; set; }

        [DisplayName("All available actions")]
        public List<CharacterActionView> AllAvailableActions
        {
            get
            {
                var allAvailableActions = new List<CharacterActionView>();
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
            set
            {
                //Should be removed. For now it will cause compile errors in fight ( it needs the SET )
            }
        }

        [DisplayName("Active statuses")]
        public List<StatusView> ActiveStatuses { get; set; }


    }
}
