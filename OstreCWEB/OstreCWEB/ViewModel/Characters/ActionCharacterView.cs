using OstreCWEB.DomainModels.CharacterModels.Enums;
using System.ComponentModel;

namespace OstreCWEB.ViewModel.Characters
{
    public class AbilityCharacterView
    {
        [DisplayName("Your action")]
        public AbilityView CharacterAction { get; set; }
        [DisplayName("Uses left:")]
        //Amount of uses before action is not available 
        public int UsesLeftBeforeRest { get; set; }
        public bool IsActionUsableInCombat
        {
            get
            {
                if (CharacterAction != null)
                {
                    if (CharacterAction.ActionType != AbilityType.Cantrip && CharacterAction.ActionType != AbilityType.WeaponAttack) { return UsesLeftBeforeRest > 0; }
                    else { return true; }
                }
                else { return false; }
            }
        }
        public bool CanShowUsesLeft
        {
            get
            {
                return this.CharacterAction.ActionType != AbilityType.Cantrip;
            }
        }
    }
}
