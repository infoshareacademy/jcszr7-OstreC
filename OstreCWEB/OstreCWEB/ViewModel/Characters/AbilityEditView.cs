using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OstreCWEB.DomainModels.CharacterModels.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.ViewModel.Characters
{
    public class AbilityEditView
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Action name")]
        [RegularExpression(@"^(?:.*[a-z]){1,20}$", ErrorMessage = "Name can't contain numbers, must be provided and can't be longer than 20 characters.")]
        public string AbilityName { get; set; }
        [DisplayName("Action description")]
        [Required]
        [RegularExpression(@"^(?:.*[a-z]){1,100}$", ErrorMessage = "Description can't contain numbers, must be provided and can't be longer than 100 characters.")]
        public string AbilityDescription { get; set; }
        [DisplayName("Action type")]
        [Required]
        [EnumDataType(typeof(AbilityType),ErrorMessage ="This ability type doesn't exist!")]
        public AbilityType ActionType { get; set; }
        [DisplayName("Saving Throw")]
        [Required]
        public bool SavingThrowPossible { get; set; }
        [DisplayName("Statistic for test")]
        [Required]
        [EnumDataType(typeof(Statistics), ErrorMessage = "This stat doesn't exist!")]
        public Statistics StatForTest { get; set; }
        [DisplayName("Max roll")]
        [Required]
        [Range(0, 50)]
        public int Max_Dmg { get; set; }
        [DisplayName("Flat bonus")]
        [Required]
        [Range(0, 20)]
        public int Flat_Dmg { get; set; }
        [DisplayName("Dice thrown amount:")]
        [Required]
        [Range(0, 20)] 
        public int Hit_Dice_Nr { get; set; }
        [DisplayName("Possible target")]
        [EnumDataType(typeof(TargetType), ErrorMessage = "Possible target provided doesn't exist!")]
        public TargetType PossibleTarget { get; set; }
        [DisplayName("Inflicts status")]
        [Required]
        public bool InflictsStatus { get; set; }
        [DisplayName("Linked Status")]
        [ValidateNever]
        public int? StatusId { get; set; }
        [DisplayName("Linked Status")]
        [ValidateNever]
        public Dictionary<int, string> AllStatuses { get; set; }
        [DisplayName("Linked Class")]
        [ValidateNever]
        public Dictionary<int, string> AllClasses { get; set; }
        //Defined for actions reseting with rest.
        [DisplayName("Uses max before rest:")]
        [Required]
        [Range(0, 5)]
        public int UsesMaxBeforeRest { get; set; }
        //Defined for items which have max use before disapearing. 
        [DisplayName("Deals damage:")]
        [Required]
        public bool AggressiveAction { get; set; }
        [ValidateNever]
        public int? PlayableClassId { get; set; }
    }
}
