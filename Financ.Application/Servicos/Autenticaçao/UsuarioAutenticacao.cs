using Financ.Application.Comun.Resultado;
using Financ.Application.Interfaces.Autenticação;
using Financ.UI.Api.Models;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Servicos.Autenticaçao
{
    public sealed class UsuarioAutenticacao : IUsuarioAutenticacao
    {
        private readonly IMediator _mediator;
        public UsuarioAutenticacao(IMediator mediator)
        {
          _mediator = mediator;
        }
        public async Task<Resultado<string>> CadastraUsuario(CadastraUsuarioDTO usuarioDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<Resultado<RetornaTokenDTO>> AutenticacaoUsuario(ConectaUsuarioDTO conectaUsuario)
        {
            throw new NotImplementedException();
        }

    }
}
