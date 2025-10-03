using Financ.Application.DTOs;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Querys
{
    public class RetornarContaIdQuery : IRequest<RetornaContasDTO>
    {
        public int IdConta { get; private set; }
        public RetornarContaIdQuery(int idConta)
        {
            IdConta = idConta;
        }
    }
}
