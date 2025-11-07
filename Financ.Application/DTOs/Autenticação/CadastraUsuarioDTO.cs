using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Financ.Application.DTOs.Autenticação
{
    public class CadastraUsuarioDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Senha", ErrorMessage = "As senhas devem ser identicas!")]
        public string ConfirmarSenha { get; set; }
    }
}
