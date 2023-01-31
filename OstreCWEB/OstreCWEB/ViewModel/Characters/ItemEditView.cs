﻿using OstreCWEB.Data.Repository.Characters.CharacterModels;
using OstreCWEB.Data.Repository.Characters.Enums;
using System.ComponentModel;

namespace OstreCWEB.ViewModel.Characters
{
    public class ItemEditView
    {

        public int ItemId { get; set; }
        [DisplayName("Item Type")]
        public ItemType ItemType { get; set; }
        [DisplayName("Armor Class")]
        public int ArmorClass { get; set; } 
        [DisplayName("Name")]
        public string? Name { get; set; }
        [DisplayName("Linked Action")] 
        public int ActionToTriggerId { get; set; }
        [DisplayName("Granted by class")]
        public int? PlayableClassId { get; set; }
        public  Dictionary<int, string> AllExistingActions { get; set; }
        public Dictionary<int, string> AllExistingClasses { get; set; }

        [DisplayName("Destroyed on use")]
        public bool DeleteOnUse { get; set; }  
        public bool isArmor
        {
            get
            {
                if (this.ItemType == ItemType.Armor || this.ItemType == ItemType.Shield)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        [DisplayName("Equipable")]
        public bool CanBeEquipped
        {
            get
            {
                return this.ItemType != ItemType.SpecialItem;
            }
        }
     
     
    }
}
