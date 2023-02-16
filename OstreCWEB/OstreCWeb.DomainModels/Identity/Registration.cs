using System.ComponentModel.DataAnnotations;

namespace OstreCWEB.DomainModels.Identity
{
    public class Registration
    {
        [Required]
        [RegularExpression("^[A-Za-z.\\s_-]+$", ErrorMessage = "Can't contain special characters")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z.\\s_-]+$", ErrorMessage = "Can't contain special characters")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum length 6 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password confirmation invalid")]
        public string PasswordConfirm { get; set; }
        [ValidateNever]
        public string? Role { get; set; }
    }
}
