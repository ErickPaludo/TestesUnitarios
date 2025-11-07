using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.UsuarioAutenticação.Commands;
using Financ.Domain.Interfaces.Autenticação;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.UsuarioAutenticação.Handler
{
    public class AutenticadoUsuarioHandler : IRequestHandler<AutenticadoUsuarioCommand, Resultado<string>>
    {
        private readonly IAutenticacao _autenticacao;
        public AutenticadoUsuarioHandler(IAutenticacao autenticacao)
        {
            _autenticacao = autenticacao;
        }
        public async Task<Resultado<string>> Handle(AutenticadoUsuarioCommand request, CancellationToken cancellationToken)
        {
            return Resultado<string>.GeraSucesso("Certo neh");//  await _autenticacao.Autenticador(request.Email, request.Senha);
        }
    }
}
