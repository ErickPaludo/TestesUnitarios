using Financ.Application.Comun.Resultado;
using Financ.Application.DTOs.ContasUsuarios.Get;
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
    public class IncluiUsuarioContaCommand : IRequest<Resultado<RetornaCadastroContasUsuariosDTO>>
    {
        public int IdConta { get; private set; }
        public Guid IdUsuario { get; private set; }
        public TiposAcessos Acesso { get; private set; }
        public TiposStatus Status { get; private set; }
        public IncluiUsuarioContaCommand(int idConta, Guid idUsuario, TiposAcessos acesso)
        {
            IdConta = idConta;
            IdUsuario = idUsuario;
            Acesso = acesso;
            Status = TiposStatus.Ativo;
        }
    }
}
