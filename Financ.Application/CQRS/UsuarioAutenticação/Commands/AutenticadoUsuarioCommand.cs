using Financ.Application.Comun.Resultado;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.UsuarioAutenticação.Commands
{
    public class AutenticadoUsuarioCommand : IRequest<Resultado<string>>
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public AutenticadoUsuarioCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}
