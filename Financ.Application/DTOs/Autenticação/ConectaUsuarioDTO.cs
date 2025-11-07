using System.ComponentModel.DataAnnotations;

namespace Financ.Application.DTOs.Autenticação
{
    public class ConectaUsuarioDTO
    {
        [Required(ErrorMessage = "Usuário inválido!")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Preencha a senha!")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max " +
            "{1} characters long.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
