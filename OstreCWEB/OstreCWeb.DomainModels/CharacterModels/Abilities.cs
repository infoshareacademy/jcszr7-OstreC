using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.ManyToMany;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.DomainModels.CharacterModels
{

    //"using" a spell , item  or weapon is an action. 
    public class Abilities
    {
        //EF config
        [Key]
        public int CharacterActionId { get; set; }
        public Status? Status { get; set; }
        public int? StatusId { get; set; }

        public List<AbilitiesCharacter>? LinkedCharacter { get; set; }
        public List<Item>? LinkedItems { get; set; }
        public int? PlayableClassId { get; set; }
        public PlayableClass? PlayableClass { get; set; }
        //

        public string ActionName { get; set; }
        public string ActionDescription { get; set; }
        public CharacterActionType ActionType { get; set; }
        public bool SavingThrowPossible { get; set; }
        public int Max_Dmg { get; set; }
        public int Flat_Dmg { get; set; }
        public int Hit_Dice_Nr { get; set; }
        public TargetType PossibleTarget { get; set; }
        public bool InflictsStatus { get; set; }
        public Statistics StatForTest { get; set; } 
        public int UsesMaxBeforeRest { get; set; } 
        public bool AggressiveAction { get; set; }
    }
}
