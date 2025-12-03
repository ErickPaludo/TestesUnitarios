using Financ.Application.Comun.Resultado;
using Financ.Application.DTOs.Usuarios.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Interfaces.Usuarios
{
    public interface IUsuariosServicos
    {
        Task<Resultado<string>> CadastraUsuario(CadastraUsuarioDTO usuarioDTO);
    }
}
