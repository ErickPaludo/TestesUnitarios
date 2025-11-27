using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Commands;
using Financ.Application.DTOs.Autenticação;
using Financ.Domain.Interfaces.Autenticação;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Handler
{
    public class AutenticadoUsuarioHandler : IRequestHandler<AutenticadoUsuarioCommand, Resultado<RetornaTokenDTO>>
    {
        private readonly IAutenticacao _autenticacao;
        public AutenticadoUsuarioHandler(IAutenticacao autenticacao)
        {
            _autenticacao = autenticacao;
        }

        public async Task<Resultado<RetornaTokenDTO>> Handle(AutenticadoUsuarioCommand request, CancellationToken cancellationToken)
        {
            bool autenticador = await _autenticacao.Autenticador(request.Email, request.Senha);
            if (autenticador)
            {
                var token = _autenticacao.GeraToken(await _autenticacao.ObtemIdUsuario(request.Email), request.Email);
                return Resultado<RetornaTokenDTO>.GeraSucesso(new RetornaTokenDTO { Token = token.email, Expiracao = token.Expiracao });
            }
            return Resultado<RetornaTokenDTO>.GeraFalha(Falha.ErroOperacional("Usuário ou senha inválidos!"));
        }
    }
}
