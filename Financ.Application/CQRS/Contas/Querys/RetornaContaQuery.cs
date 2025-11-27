using Financ.Application.Comun.Resultado;
using Financ.Application.DTOs;
using Financ.Application.DTOs.Contas;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Querys
{
    public class RetornaContaQuery : IRequest<Resultado<List<RetornaContasDTO>>>
    {
        public Guid IdUsuario { get; private set; }
        public FiltroContasDTO? Filtros { get; private set; }
        public RetornaContaQuery(Guid idUsuario, FiltroContasDTO? filtros)
        {
            IdUsuario = idUsuario;
            Filtros = filtros;
        }
    }
}
