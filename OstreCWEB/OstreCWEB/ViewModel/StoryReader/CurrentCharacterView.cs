using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.ViewModel.Characters;

namespace OstreCWEB.ViewModel.StoryReader
{
    public class CurrentCharacterView
    {
        public string CharacterName { get; set; }
        public int MaxHealthPoints { get; set; }
        public int CurrentHealthPoints { get; set; }
        public int Strenght { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public PlayableRaceView Race { get; set; }
        public PlayableClassView CharacterClass { get; set; }
        public List<ItemCharacterView> LinkedItems { get; set; }
        public List<AbilityCharacterView> LinkedAbilities { get; set; }
        public List<StatusView> ActiveStatuses { get; set; }

        public IEnumerable<ItemCharacterView> ItemCharacterWithAction
        {
            get
            {
                var result = new List<ItemCharacterView>();
                foreach (var item in LinkedItems)
                {
                    if (item.Item.ItemType != ItemType.SpecialItem && item.IsEquipped && item.Item.ActionToTrigger != null)
                    {
                        result.Add(item);
                    }
                    if (item.Item.ItemType == ItemType.SpecialItem && item.Item.ActionToTrigger != null)
                    {
                        result.Add(item);
                    }
                }
                return result;
            }
        }
        public int CombatId { get; set; }
        public int NotEquippedItemsCount
        {
            get
            {
                return LinkedItems.Where(i => !i.IsEquipped).Count();
            }
        }
        public int CurrentHealthPercentage { get { return CalculateCurrentHealthPercentage(); } }

        public int CalculateCurrentHealthPercentage()
        {
            return (CurrentHealthPoints * 100) / MaxHealthPoints;
        }

        public bool HasShowableActions { get { return LinkedAbilities.Where(a => a.IsActionUsableInCombat).Count() > 0; } }

    }
}
