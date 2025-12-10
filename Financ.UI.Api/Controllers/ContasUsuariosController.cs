using Financ.Application.DTOs.ContasUsuarios.Get.Filtros;
using Financ.Application.DTOs.ContasUsuarios.Patch;
using Financ.Application.DTOs.ContasUsuarios.Post;
using Financ.Application.Interfaces.ContasUsuarios;
using Financ.Domain.Interfaces.Repositorios;
using Financ.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Financ.UI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContasUsuariosController : ControllerBase
    {
        private readonly IContasUsuariosServicos _contasUsuariosServico;
        public ContasUsuariosController(IContasUsuariosServicos contasUsuariosServico)
        {
            _contasUsuariosServico = contasUsuariosServico;
        }

        [HttpPost("entrar_na_conta")]
        public async Task<IActionResult> EntrarNaConta(InclusaoContaUsuarioDTO inclusaoContaUsuarioDTO)
        {
            var usuarioConta = await _contasUsuariosServico.IncluiUsuarioNaConta(inclusaoContaUsuarioDTO,User.RetornaIdUsuario());
            return usuarioConta.RetornoAutomatico();
        }
        [HttpGet("retorna_usuarios_associados")]
        public async Task<IActionResult> RetornaUsuarosAssociados([FromQuery] FiltroUsuarioAssociado filtroConta)
        {
            var usuariosAssociados = await _contasUsuariosServico.RetornaUsuariosAssociados(filtroConta, User.RetornaIdUsuario());
            return usuariosAssociados.RetornoAutomatico();
        }
        [HttpPatch("altera_usuario_conta/{idConta}")]
        public async Task<IActionResult> AlteraUsuarioConta(int idConta, [FromBody] AtualizaContasUsuariosDTO contaUsuario)
        {
            var usuarioAlterado = await _contasUsuariosServico.AtualizaUsuarioConta( User.RetornaIdUsuario(), idConta, contaUsuario);
            return usuarioAlterado.RetornoAutomatico();
        }
    }
}
