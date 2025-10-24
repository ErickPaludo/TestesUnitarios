using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Base.Command;
using Financ.Domain.Entidades;
using Financ.Domain.Enums;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Commands
{
    public class IncluiUsuarioContaCommand : BaseCommand, IRequest<Resultado<ContasUsuarios>>
    {
        public IncluiUsuarioContaCommand(int idConta, Guid idUsuario, TiposAcessos acesso)
        {
            IdConta = idConta;
            IdUsuario = idUsuario;
            Acesso = acesso;
            Status = TiposStatus.Ativo;
        }
    }
}
