using Financ.Application.Comun.Resultado;
using Financ.Application.DTOs.ContasUsuarios;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Querys
{
    public class RetornaUsuariosAssociadosQuery : IRequest<Resultado<List<RetornaUsuariosAssociadosDTO>>>
    {
        public int IdConta { get; private set; }
        public Guid IdUsuario { get; private set; }
        public RetornaUsuariosAssociadosQuery(int idConta,Guid idUsuario)
        {
            IdConta = idConta;
            IdUsuario = idUsuario;
        }
    }
}
