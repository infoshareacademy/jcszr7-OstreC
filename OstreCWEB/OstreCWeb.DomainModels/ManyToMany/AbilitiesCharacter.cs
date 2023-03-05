using OstreCWEB.DomainModels.CharacterModels;
namespace OstreCWEB.DomainModels.ManyToMany
{
    public class AbilitiesCharacter
    {
        //Ef config

        public Ability CharacterAction { get; set; }

        public int CharacterActionId { get; set; }

        public Character Character { get; set; }
        public int CharacterId { get; set; }
        //Amount of uses before action is not available 
        public int UsesLeftBeforeRest { get; set; }
    }
}
