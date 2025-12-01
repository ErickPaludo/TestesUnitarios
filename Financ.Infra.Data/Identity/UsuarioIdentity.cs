using Financ.Domain.Interfaces.InterfaceEntidades;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Infra.Data.Identity
{
    public class UsuarioIdentity : IdentityUser, IUsuarioIdentity
    {
        public string PrimeiroNome {  get; set; } = string.Empty;
        public string SegundoNome { get; set; } = string.Empty;
    }
}
