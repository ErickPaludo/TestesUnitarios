using Financ.Application.Comun.Resultado;
using Financ.UI.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Interfaces.Autenticação
{
    public interface IUsuarioAutenticacao
    {
        Task<Resultado<string>> CadastraUsuario(CadastraUsuarioDTO usuarioDTO);
        Task<Resultado<RetornaTokenDTO>> AutenticacaoUsuario(ConectaUsuarioDTO conectaUsuario);
    }
}
