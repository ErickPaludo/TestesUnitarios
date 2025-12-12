using Financ.Application.Comun.Resultado;
using Financ.Application.DTOs.ContasUsuarios.Get;
using Financ.Application.DTOs.ContasUsuarios.Get.Filtros;
using Financ.Application.DTOs.ContasUsuarios.Patch;
using Financ.Application.DTOs.ContasUsuarios.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Interfaces.ContasUsuarios
{
    public interface IContasUsuariosServicos
    {
        Task<Resultado<RetornaCadastroContasUsuariosDTO>> IncluiUsuarioNaConta(InclusaoContaUsuarioDTO contaUsuarioDTO, string idUsuario);
        Task<Resultado<List<RetornaUsuariosAssociadosDTO>>> RetornaUsuariosAssociados(FiltroUsuarioAssociado filtroConta, string idUsuarioSolicitante);
        Task<Resultado<RetornaCadastroContasUsuariosDTO>> AtualizaUsuarioConta(string idUsuarioSolicitante, string idUsuarioAlterado, int idConta, AtualizaContasUsuariosDTO contaUsuario);
    }
}
