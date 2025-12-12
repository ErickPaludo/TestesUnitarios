using Financ.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Interfaces.Autenticação
{
    public interface IUsuariosIdentityServicos
    {
        Task<string> ObtemIdUsuario(string email);
        Task<(bool, string?)> RegistrarUsuario(Usuario usuario, string senha);
        Task<Usuario> ObtemUsuario(string idUsuario);
    }
}
