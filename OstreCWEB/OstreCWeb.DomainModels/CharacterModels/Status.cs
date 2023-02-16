using OstreCWEB.DomainModels.CharacterModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.DomainModels.CharacterModels
{
    public class Status
    {
        //EF config
        [Key]
        public int StatusId { get; set; }
        public List<Abilities> CharacterActions { get; set; }
        //
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusType StatusType { get; set; }
    }
}
