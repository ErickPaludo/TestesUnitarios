using Financ.Application.DTOs.ContasUsuarios;
using Financ.Application.Interfaces.ContasUsuarios;
using Financ.Domain.Interfaces.Repositorios;
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
            await _contasUsuariosServico.IncluiUsuarioNaConta(inclusaoContaUsuarioDTO);
            return Ok("Entrou na conta com sucesso!");
        }
    }
}
