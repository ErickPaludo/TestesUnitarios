using Financ.Application.DTOs.Usuarios;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Financ.Application.DTOs.Autenticação.Post
{
    public class ConectaUsuarioDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string Senha { get; set; }
    }
}
