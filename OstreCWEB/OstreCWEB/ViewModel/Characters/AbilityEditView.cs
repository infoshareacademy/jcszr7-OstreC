﻿using OstreCWEB.DomainModels.CharacterModels.Enums;
using System.ComponentModel;

namespace OstreCWEB.ViewModel.Characters
{
    public class AbilityEditView
    {
        public int Id { get; set; }
        [DisplayName("Action name")]
        public string AbilityName { get; set; }
        [DisplayName("Action description")]
        public string AbilityDescription { get; set; }
        [DisplayName("Action type")]
        public AbilityType ActionType { get; set; }
        [DisplayName("Saving Throw")]
        public bool SavingThrowPossible { get; set; }
        [DisplayName("Statistic for test")]
        public Statistics StatForTest { get; set; }
        [DisplayName("Max roll")]
        public int Max_Dmg { get; set; }
        [DisplayName("Flat bonus")]
        public int Flat_Dmg { get; set; }
        [DisplayName("Dice thrown amount:")]
        public int Hit_Dice_Nr { get; set; }
        [DisplayName("Possible target")]
        public TargetType PossibleTarget { get; set; }
        [DisplayName("Inflicts status")]
        public bool InflictsStatus { get; set; }
        [DisplayName("Linked Status")]
        public int? StatusId { get; set; }
        [DisplayName("Linked Status")]
        public Dictionary<int, string> AllStatuses { get; set; }
        [DisplayName("Linked Class")]
        public Dictionary<int, string> AllClasses { get; set; }
        //Defined for actions reseting with rest.
        [DisplayName("Uses max before rest:")]
        public int UsesMaxBeforeRest { get; set; }
        //Defined for items which have max use before disapearing. 
        [DisplayName("Deals damage:")]
        public bool AggressiveAction { get; set; }
        public int? PlayableClassId { get; set; }
    }
}