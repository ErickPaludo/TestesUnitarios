using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Financ.UI.Api.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords don´t match")]
        public string ConfirmPassword { get; set; }
    }
}
