using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.UsuarioAutenticação.Commands;
using Financ.Application.DTOs.Autenticação;
using Financ.Application.Interfaces.Autenticação;
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

        public async Task<Resultado<RetornaTokenDTO>> AutenticacaoUsuario(ConectaUsuarioDTO conectaUsuario)
        {
            await _mediator.Send(new AutenticadoUsuarioCommand(conectaUsuario.Email, conectaUsuario.Senha));
            return null;
        }

        public async Task<Resultado<string>> CadastraUsuario(CadastraUsuarioDTO usuarioDTO)
        {
             return await _mediator.Send(new CadastraUsuarioCommand(usuarioDTO.Email, usuarioDTO.Senha, usuarioDTO.ConfirmarSenha));
        }
    }
}
