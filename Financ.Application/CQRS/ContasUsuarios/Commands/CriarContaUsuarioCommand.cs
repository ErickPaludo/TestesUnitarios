using Financ.Application.Comun.Resultado;
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
    public class CriarContaUsuarioCommand : IRequest<Resultado<ContasUsuarios>>
    {
        public int IdConta { get; private set; }
        public Guid IdUsuario { get; private set; }
        public TiposAcessos Acesso { get; private set; }
        public TiposStatus Status { get; private set; }
        public CriarContaUsuarioCommand(int idConta, Guid idUsuario)
        {
            IdConta = idConta;
            IdUsuario = idUsuario;
            Acesso = TiposAcessos.Administrador;
            Status = TiposStatus.Ativo;
        }
    }
}
