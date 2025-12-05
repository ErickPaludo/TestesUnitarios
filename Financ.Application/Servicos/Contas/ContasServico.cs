using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Commands;
using Financ.Application.CQRS.Querys;
using Financ.Application.DTOs.Contas.Get;
using Financ.Application.DTOs.Contas.Get.Filtros;
using Financ.Application.DTOs.Contas.Ptch;
using Financ.Application.Interfaces.Contas;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Servicos.Contas
{
    public class ContasServico : IContasServicos
    {
        private readonly IMediator _mediator;
        public ContasServico(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Resultado<RetornaContasDTO>> CriarConta(Guid idUsuario, CadastrarContasDTO contaDTO)
        {
            var commandConta = new CriarContaCommand(contaDTO.Titulo, contaDTO.CreditoAtivo, contaDTO.CreditoLimite, contaDTO.DiaFechamento, contaDTO.DiaVencimento, contaDTO.CreditoMaximo);

            var conta = await _mediator.Send(commandConta);

            if (!conta.ValidaSucesso)
                return conta;

            var contaUsuario = await _mediator.Send(new CriarContaUsuarioCommand(conta.Sucesso!.IdConta, idUsuario));

            if (contaUsuario.ValidaFalha)
                return Resultado<RetornaContasDTO>.GeraFalha(contaUsuario.Falha!);

            return conta;
        }

        public async Task<Resultado<List<RetornaContasDTO>>> RetornarContas(FiltroContasDTO? filtros, Guid IdUsuario)
        {
            return await _mediator.Send(new RetornaContaQuery(IdUsuario, filtros));
        }

        public async Task<Resultado<RetornaContasDTO>> AlterarConta(int idContaUsuario, Guid IdUsuario, AtualizaContaDTO contaDTO)
        {
            var contaAtualizada = await _mediator.Send(new AtualizarContaCommand(idContaUsuario, IdUsuario, contaDTO.CreditoAtivo, contaDTO.CreditoLimite, contaDTO.Status, contaDTO.Titulo, contaDTO.DiaFechamento, contaDTO.DiaVencimento, contaDTO.CreditoMaximo));

            if (contaAtualizada.ValidaSucesso)
                return Resultado<RetornaContasDTO>.GeraSucesso(contaAtualizada.Sucesso!);
            else
                return Resultado<RetornaContasDTO>.GeraFalha(contaAtualizada.Falha!);
        }
    }
}
