using Financ.Application.DTOs.ContasUsuarios;
using Financ.Application.Interfaces.ContasUsuarios;
using Financ.Domain.Interfaces.Repositorios;
using Financ.UI.Api.Extensao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Financ.UI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var usuarioConta = await _contasUsuariosServico.IncluiUsuarioNaConta(inclusaoContaUsuarioDTO);
            return usuarioConta.RetornoAutomatico();
        }
    }
}
