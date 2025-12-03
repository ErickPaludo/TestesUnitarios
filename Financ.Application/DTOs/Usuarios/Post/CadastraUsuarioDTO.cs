using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Financ.Domain.Validacoes.Mensagens;

namespace Financ.Application.DTOs.Usuarios.Post
{
    public class CadastraUsuarioDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = MensagensUsuarios.PRIMEIRO_NOME_MINIMO)]
        [MaxLength(100, ErrorMessage = MensagensUsuarios.PRIMEIRO_NOME_MAXIMO)]
        public string PrimeiroNome { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = MensagensUsuarios.SEGUNDO_NOME_MINIMO)]
        [MaxLength(100, ErrorMessage = MensagensUsuarios.SEGUNDO_NOME_MAXIMO)]
        public string SegundoNome { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string Senha { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Senha", ErrorMessage = "As senhas devem ser identicas!")]
        public string ConfirmarSenha { get; set; }
    }
}
