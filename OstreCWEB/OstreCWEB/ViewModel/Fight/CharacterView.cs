﻿using OstreCWEB.Data.Repository.Characters;
using OstreCWEB.Data.Repository.Items;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.ViewModel.Fight
{
    public class CharacterView
    {
        public int CombatId { get; set; }
        public string CharacterName { get; set; }
        public int HealthPoints { get; set; }
        public int Level { get; set; } 
        public Item EquippedArmor { get; set; } 
        public Item EquippedWeapon { get; set; } 
        public Item EquippedSecondaryWeapon { get; set; } 
        public List<CharacterActions> AllAvailableActions { get; set; } 
        public List<Status> ActiveStatuses { get; set; }

    }
}
