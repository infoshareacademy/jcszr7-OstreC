using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.DomainModels.Identity
{
    public class Login
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
