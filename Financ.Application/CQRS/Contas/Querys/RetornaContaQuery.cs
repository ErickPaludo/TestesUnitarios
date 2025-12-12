using Financ.Application.Comun.Resultado;
using Financ.Application.DTOs.Contas.Get;
using Financ.Application.DTOs.Contas.Get.Filtros;
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
        public string IdUsuario { get; private set; }
        public FiltroContasDTO? Filtros { get; private set; }
        public RetornaContaQuery(string idUsuario, FiltroContasDTO? filtros)
        {
            IdUsuario = idUsuario;
            Filtros = filtros;
        }
    }
}
