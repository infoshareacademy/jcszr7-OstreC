using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.DomainModels.CharacterModels.Enums
{
    public enum TargetType
    {
        [Display(Name = "Yourself")]
        Caster = 1,
        [Display(Name = "Enemy")]
        Target = 2
    }
}
