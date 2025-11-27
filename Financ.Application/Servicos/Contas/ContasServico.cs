using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Commands;
using Financ.Application.CQRS.Querys;
using Financ.Application.DTOs;
using Financ.Application.DTOs.Contas;
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
            var commandConta = new CriarContaCommand(contaDTO.Titulo, contaDTO.DiaFechamento, contaDTO.DiaVencimento, contaDTO.CreditoLimite);
            var conta = await _mediator.Send(commandConta);

            if (!conta.ValidaSucesso)
                return Resultado<RetornaContasDTO>.GeraFalha(conta.Falha!);

            var commandContaUsuario = new CriarContaUsuarioCommand(conta.Sucesso!.Id, idUsuario);
            await _mediator.Send(commandContaUsuario);
            return Resultado<RetornaContasDTO>.GeraSucesso(new RetornaContasDTO
            {
                IdConta = conta.Sucesso!.Id,
                Titulo = conta.Sucesso!.Titulo,
                DiaFechamento = conta.Sucesso!.DiaFechamento,
                DiaVencimento = conta.Sucesso!.DiaVencimento,
                CreditoLimite = conta.Sucesso!.CreditoLimite
            });
        }

        public async Task<Resultado<List<RetornaContasDTO>>> RetornarContas(FiltroContasDTO? filtros, Guid IdUsuario)
        { 
          return await _mediator.Send(new RetornaContaQuery(IdUsuario,filtros));
        }

        public async Task<Resultado<RetornaContasDTO>> AlterarConta(int idContaUsuario, Guid IdUsuario, AtualizaContaDTO contaDTO)
        {
            var commandConta = await _mediator.Send(new AtualizarContaCommand(idContaUsuario, IdUsuario, contaDTO.Status, contaDTO.Titulo, contaDTO.DiaFechamento, contaDTO.DiaVencimento, contaDTO.CreditoLimite));

            if (commandConta.ValidaSucesso)
                return Resultado<RetornaContasDTO>.GeraSucesso(new RetornaContasDTO
                {
                    IdConta = commandConta.Sucesso!.Id,
                    Titulo = commandConta.Sucesso!.Titulo,
                    DiaFechamento = commandConta.Sucesso!.DiaFechamento,
                    DiaVencimento = commandConta.Sucesso!.DiaVencimento,
                    CreditoLimite = commandConta.Sucesso!.CreditoLimite
                });
            else
                return Resultado<RetornaContasDTO>.GeraFalha(commandConta.Falha!);
        }
    }
}
