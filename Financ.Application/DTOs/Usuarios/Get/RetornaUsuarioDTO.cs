using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.DTOs.Usuarios.Get
{
    public class RetornaUsuarioDTO
    {
        public string Id { get; set; }
        public string PrimeiroNome { get; set; }
        public string SegundoNome { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
    }
}
