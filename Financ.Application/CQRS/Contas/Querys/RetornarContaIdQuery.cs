using Financ.Application.Comun.Resultado;
using Financ.Application.DTOs;
using Financ.Domain.Entidades;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Querys
{
    public class RetornarContaIdQuery : IRequest<Resultado<Contas>>
    {
        public int IdConta { get; private set; }
        public RetornarContaIdQuery(int idConta)
        {
            IdConta = idConta;
        }
    }
}
