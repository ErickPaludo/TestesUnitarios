using Financ.Application.DTOs;
using Financ.Application.Interfaces.Contas;
using Financ.UI.Api.Extensao;
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
        public async Task<IActionResult> CadastrarContas(CadastrarContasDTO contasDTO)
        {
           var conta = await _contaServico.CriarConta(contasDTO);
            return conta.RetornoAutomatico("rota");
        }
        [HttpGet("retorna_contas/{id:int}")]
        public async Task<IActionResult> RetornarContas(int id)
        {
            var contasLista = await _contaServico.RetornarContas(id);
            return contasLista.RetornoAutomatico();
        }
    }
}
