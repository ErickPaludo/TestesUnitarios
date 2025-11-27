using Financ.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Interfaces.Autenticação
{
    public interface IAutenticacao
    {
        Task<bool> Autenticador(string email, string senha);
        Task<string> ObtemIdUsuario(string email);
        Task<bool> RegistrarUsuario(string email, string senha);
        Task<Usuario> ObtemUsuario(Guid idUsuario);
       (string email, DateTime Expiracao) GeraToken(string id,string email);
    }
}
