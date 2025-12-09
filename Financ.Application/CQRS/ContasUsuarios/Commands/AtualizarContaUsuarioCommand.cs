using Financ.Application.Comun.Resultado;
using Financ.Application.DTOs.ContasUsuarios.Get;
using Financ.Domain.Enums;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Commands
{
    public record AtualizarContaUsuarioCommand(Guid idUsuario, int idConta, TiposAcessos? acesso, TiposStatus? status) : IRequest<Resultado<RetornaCadastroContasUsuariosDTO>>;
}
