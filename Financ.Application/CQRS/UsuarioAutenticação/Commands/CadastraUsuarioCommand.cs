using Financ.Application.Comun.Resultado;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.UsuarioAutenticação.Commands
{
    public class CadastraUsuarioCommand : IRequest<Resultado<string>>
    {
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string ConfirmarSenha { get; set; }
        public CadastraUsuarioCommand(string email, string senha, string confirmarSenha)
        {
            Email = email;
            Senha = senha;
            ConfirmarSenha = confirmarSenha;
        }
    }
}
