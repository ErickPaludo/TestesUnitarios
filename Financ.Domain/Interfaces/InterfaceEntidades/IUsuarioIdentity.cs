using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Interfaces.InterfaceEntidades
{
    public interface IUsuarioIdentity
    {
        public string PrimeiroNome { get; }
        public string SegundoNome { get; }
    }
}
