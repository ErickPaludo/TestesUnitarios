using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Commands;
using Financ.Application.DTOs.Autenticação.Get;
using Financ.Application.DTOs.Autenticação.Post;
using Financ.Application.Interfaces.Autenticação;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Servicos.Autenticaçao
{
    public sealed class AutenticacaoServicos : IAutenticacaoServicos
    {
        private readonly IMediator _mediator;
        public AutenticacaoServicos(IMediator mediator)
        {
          _mediator = mediator;
        }

        public async Task<Resultado<RetornaTokenDTO>> AutenticacaoUsuario(ConectaUsuarioDTO conectaUsuario)
        {
           var tokenAutenticacao = await _mediator.Send(new AutenticadoUsuarioCommand(conectaUsuario.Email, conectaUsuario.Senha));
            if (tokenAutenticacao.ValidaSucesso)
            {
                return Resultado<RetornaTokenDTO>.GeraSucesso(new RetornaTokenDTO{Token = tokenAutenticacao.Sucesso!.Token,Expiracao = tokenAutenticacao.Sucesso.Expiracao});
            }
            return Resultado<RetornaTokenDTO>.GeraFalha(tokenAutenticacao.Falha!);
        }
    }
}
