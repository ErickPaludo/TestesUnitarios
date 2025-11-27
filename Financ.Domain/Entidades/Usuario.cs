using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Entidades
{
    public class Usuario
    {
        public Guid IdUsuario { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }

        public Usuario(Guid idUsuario, string nome, string email)
        {
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
        }
    }
}
