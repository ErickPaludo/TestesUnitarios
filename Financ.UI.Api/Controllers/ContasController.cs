using Financ.Application.DTOs.Contas;
using Financ.Application.Interfaces.Contas;
using Microsoft.AspNetCore.Mvc;

namespace Financ.UI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContasController : ControllerBase
    {
      
        private readonly IContasServicos _contaServico;

        public ContasController(IContasServicos contaServico)
        {
            _contaServico = contaServico;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarContas(ContasDTO contasDTO)
        {
            await _contaServico.CriarConta(contasDTO);
            return Created();
        }
    }
}
