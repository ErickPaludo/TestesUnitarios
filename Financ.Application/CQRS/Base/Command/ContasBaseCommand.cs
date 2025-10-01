using Financ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Base.Command
{
    public class ContasBaseCommand
    {
        public int IdConta { get; protected set; }
        public Guid IdUsuario { get; protected set; }
        public TiposAcessos Acesso { get; protected set; }
        public TiposStatus Status { get; protected set; }
        public DateTime DthrReg { get; protected set; }
    }
}
