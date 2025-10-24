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
    public class CadastraUsuarioHandler : IRequestHandler<CadastraUsuarioCommand, Resultado<string>>
    {
        private readonly IAutenticacao _autenticacao;
        public CadastraUsuarioHandler(IAutenticacao autenticacao)
        {
            _autenticacao = autenticacao;
        }
        public async Task<Resultado<string>> Handle(CadastraUsuarioCommand request, CancellationToken cancellationToken)
        {
            return Resultado<string>.GeraSucesso("ok");
        }
    }
}
