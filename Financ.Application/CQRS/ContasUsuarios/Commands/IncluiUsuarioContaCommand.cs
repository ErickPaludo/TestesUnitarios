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
    public record IncluiUsuarioContaCommand(int IdConta, string IdUsuario, TiposAcessos Acesso) : IRequest<Resultado<RetornaCadastroContasUsuariosDTO>>;
}
