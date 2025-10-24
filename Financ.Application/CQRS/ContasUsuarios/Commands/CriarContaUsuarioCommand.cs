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
    public class CriarContaUsuarioCommand : BaseCommand, IRequest<Resultado<ContasUsuarios>>
    {
        public CriarContaUsuarioCommand(int idConta, Guid idUsuario)
        {
            IdConta = idConta;
            IdUsuario = idUsuario;
            Acesso = TiposAcessos.Administrador;
            Status = TiposStatus.Ativo;
        }
    }
}
