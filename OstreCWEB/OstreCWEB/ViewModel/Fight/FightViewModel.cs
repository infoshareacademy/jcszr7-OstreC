using OstreCWEB.ViewModel.Characters;
using OstreCWEB.ViewModel.StoryReader;
using System.ComponentModel;

namespace OstreCWEB.ViewModel.Fight
{
    public class FightViewModel
    {
        [DisplayName("Your character")]
        public CurrentCharacterView ActivePlayer { get; set; }
        [DisplayName("Your enemies")]
        public List<CharacterView> ActiveEnemies { get; set; }
        [DisplayName("Fight logs")]
        public List<string> FightHistory { get; set; }
        [DisplayName("Chosen action")]
        public CharacterActionView ActiveAction { get; set; }
        [DisplayName("Chosen Target")]
        public CharacterView ActiveTarget { get; set; }
        [DisplayName("Turn number")]
        public int TurnNumber { get; set; }
        [DisplayName("Actions left")]
        public int PlayerActionCounter { get; set; }
        public bool CanShowActiveAction { get { return this.ActiveAction != null && this.ActiveAction.ActionName != null; } }
        public bool CanShowActiveTarget { get { return this.ActiveTarget != null && this.ActiveTarget.CombatId != null; } }
        public bool CanCommitAction { get { return this.ActiveTarget != null && this.ActiveTarget.CombatId != null && this.ActiveTarget.CombatId != 0 && this.ActiveAction != null; } }
    }
}
