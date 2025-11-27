using Financ.Application.DTOs;
using Financ.Application.DTOs.Contas;
using Financ.Application.Interfaces.Contas;
using Financ.Domain.Interfaces.Autenticação;
using Financ.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Financ.UI.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ContasController : ControllerBase
    {
      
        private readonly IContasServicos _contaServico;
        private readonly IAutenticacao _autenticacao;
        public ContasController(IContasServicos contaServico, IAutenticacao autenticacao)
        {
            _contaServico = contaServico;
            _autenticacao = autenticacao;
        }
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarContas(CadastrarContasDTO contasDTO)
        {
           var conta = await _contaServico.CriarConta(User.RetornaIdUsuario(), contasDTO);
            return conta.RetornoAutomatico();
        }
        [HttpGet("retorna_contas/{id:int}")]
        public async Task<IActionResult> RetornarContas(int id)
        {
            var contasLista = await _contaServico.RetornarContas(id);
            return contasLista.RetornoAutomatico();
        }

        [HttpPatch("atualiza_conta/{idContaUsuario}")]
        public async Task<IActionResult> AtualizaConta(int idContaUsuario, AtualizaContaDTO contaDTO)
        {
            var contaAtualizada = await _contaServico.AlterarConta(idContaUsuario,User.RetornaIdUsuario(), contaDTO);
            return contaAtualizada.RetornoAutomatico();
        }
    }
}
