using Financ.Application.Comun.Resultado;
using Financ.Application.DTOs.Autenticação.Get;
using Financ.Application.DTOs.Autenticação.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Interfaces.Autenticação
{
    public interface IAutenticacaoServicos
    {
        Task<Resultado<RetornaTokenDTO>> AutenticacaoUsuario(ConectaUsuarioDTO conectaUsuario);
    }
}
