using OstreCWEB.DomainModels.CharacterModels.Enums;
using System.ComponentModel;

namespace OstreCWEB.ViewModel.Characters
{
    public class ActionCharacterView
    {
        [DisplayName("Your action")]
        public CharacterActionView CharacterAction { get; set; }
        [DisplayName("Uses left:")]
        //Amount of uses before action is not available 
        public int UsesLeftBeforeRest { get; set; }
        public bool IsActionUsableInCombat
        {
            get
            {
                if (CharacterAction != null)
                {
                    if (CharacterAction.ActionType != CharacterActionType.Cantrip && CharacterAction.ActionType != CharacterActionType.WeaponAttack) { return UsesLeftBeforeRest > 0; }
                    else { return true; }
                }
                else { return false; }
            }
        }
        public bool CanShowUsesLeft
        {
            get
            {
                return this.CharacterAction.ActionType != CharacterActionType.Cantrip;
            }
        }
    }
}
