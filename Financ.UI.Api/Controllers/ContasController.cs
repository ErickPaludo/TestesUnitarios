using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Commands;
using Financ.Application.CQRS.Querys;
using Financ.Application.DTOs.Contas.Get;
using Financ.Application.DTOs.Contas.Get.Filtros;
using Financ.Application.DTOs.Contas.Ptch;
using Financ.Domain.Entidades;
using Financ.Domain.Interfaces.Autenticação;
using Financ.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.SimpleMediator;
using System.Security.Claims;

namespace Financ.UI.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ContasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContasController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarContas(CadastrarContasDTO contaDTO)
        {
            var conta = await _mediator.Send(new CriarContaCommand(User.RetornaIdUsuario(), contaDTO.Titulo, contaDTO.CreditoAtivo, contaDTO.CreditoLimite, contaDTO.DiaFechamento, contaDTO.DiaVencimento, contaDTO.CreditoMaximo));
            return conta.RetornoAutomatico();
        }
        [HttpGet("retorna_contas")]
        public async Task<IActionResult> RetornarContas([FromQuery]FiltroContasDTO? parametros)
        {
            var contasLista = await _mediator.Send(new RetornaContaQuery(User.RetornaIdUsuario(), parametros));
            return contasLista.RetornoAutomatico();
        }

        [HttpPatch("atualiza_conta/{idContaUsuario}")]
        public async Task<IActionResult> AtualizaConta(int idContaUsuario, AtualizaContaDTO contaDTO)
        {

            var contaAtualizada = await _mediator.Send(new AtualizarContaCommand(idContaUsuario, User.RetornaIdUsuario(), contaDTO.CreditoAtivo, contaDTO.CreditoLimite, contaDTO.Status, contaDTO.Titulo, contaDTO.DiaFechamento, contaDTO.DiaVencimento, contaDTO.CreditoMaximo));
            return contaAtualizada.RetornoAutomatico();
        }
    }
}
