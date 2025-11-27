using System.ComponentModel.DataAnnotations;

namespace Financ.Application.DTOs.Autenticação
{
    public class ConectaUsuarioDTO
    {
        [Required(ErrorMessage = "Usuário inválido!")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Preencha a senha!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
