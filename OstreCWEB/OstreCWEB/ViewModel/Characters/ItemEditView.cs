using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OstreCWEB.DomainModels.CharacterModels.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.ViewModel.Characters
{
    public class ItemEditView
    {
        
        public int Id { get; set; }
        [Required]
        [DisplayName("Item Type")]
        [EnumDataType(typeof(ItemType),ErrorMessage ="Provided item type doesn't exist! ")] 
        public ItemType ItemType { get; set; }
        [Required]
        [DisplayName("Armor Class")]
        [Range(0,20)] 
        public int ArmorClass { get; set; }
        [Required]
        [DisplayName("Name")]
        [RegularExpression(@"^(?:.*[a-z]){1,20}$", ErrorMessage = "Name can't contain numbers, must be provided and can't be longer than 20 characters.")]
        public string? Name { get; set; }
        [ValidateNever]
        [DisplayName("Linked Action")]
        public int? AbilityId { get; set; }
        [DisplayName("Granted by class")]
        [ValidateNever]
        public int? PlayableClassId { get; set; }
        [ValidateNever]
        public Dictionary<int, string> AllExistingActions { get; set; }
        [ValidateNever]
        public Dictionary<int, string> AllExistingClasses { get; set; }

        [DisplayName("Destroyed on use")]
        [Required]
        public bool DeleteOnUse { get; set; }
        [ValidateNever]
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
        [ValidateNever]
        public bool CanBeEquipped
        {
            get
            {
                return this.ItemType != ItemType.SpecialItem;
            }
        }


    }
}
