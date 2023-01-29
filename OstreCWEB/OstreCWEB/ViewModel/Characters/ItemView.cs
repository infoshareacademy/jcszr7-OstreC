﻿using OstreCWEB.Data.Repository.Characters.Enums;
using System.ComponentModel;

namespace OstreCWEB.ViewModel.Characters
{
    public class ItemView
    {

        public int ItemId { get; set; }
        [DisplayName("Item Type")]
        public ItemType ItemType { get; set; }
        [DisplayName("Armor Class")]
        public int ArmorClass { get; set; }
        [DisplayName("Armor Type")]
        public string ArmorType { get; set; }
        [DisplayName("Name")]
        public string? Name { get; set; }
        [DisplayName("Linked Action")]
        public  CharacterActionView ActionToTrigger { get; set; }
        [DisplayName("Destroyed on use")]
        public bool DeleteOnUse { get; set; } 
        public bool IsActionShowable 
        {
            get
            {
                if(this.ActionToTrigger != null)
                {
                    return true;
                }
                return false;
            }  
        }
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
     
     
    }
}
