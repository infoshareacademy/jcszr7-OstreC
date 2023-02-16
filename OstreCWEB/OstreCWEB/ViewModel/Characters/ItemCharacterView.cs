using System.ComponentModel;

namespace OstreCWEB.ViewModel.Characters
{
    public class ItemCharacterView
    {
        public int Id { get; set; }

        [DisplayName("Linked items")]
        public ItemView Item { get; set; }

        //Paremeters for items in the given relationship

        [DisplayName("Euipped")]
        public bool IsEquipped { get; set; }

    }
}
