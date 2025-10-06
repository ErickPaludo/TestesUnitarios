using Financ.Application.Comun.Resultadoado;
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


        public async Task<Resultado<RetornaContasDTO>> CriarConta(CadastrarContasDTO contaDTO)
        {
            var commandConta = new CriarContaCommand(contaDTO.Titulo, contaDTO.DiaFechamento, contaDTO.DiaVencimento, contaDTO.CreditoLimite);
            var conta = await _mediator.Send(commandConta);

            if (!conta.ValidaSucesso)
                return Resultado<RetornaContasDTO>.GeraFalha(conta.Falha!);

            var commandContaUsuario = new CriarContaUsuarioCommand(conta.Sucesso!.Id, Guid.NewGuid());
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

        public async Task<Resultado<RetornaContasDTO>> RetornarContas(int idContas)
        {
            var conta = await _mediator.Send(new RetornarContaIdQuery(idContas));
            if (conta.ValidaSucesso)
            {
                return Resultado<RetornaContasDTO>.GeraSucesso(new RetornaContasDTO
                {
                    IdConta = conta.Sucesso!.Id,
                    Titulo = conta.Sucesso!.Titulo,
                    DiaFechamento = conta.Sucesso!.DiaFechamento,
                    DiaVencimento = conta.Sucesso!.DiaVencimento,
                    CreditoLimite = conta.Sucesso!.CreditoLimite
                });
            }
            else
                return Resultado<RetornaContasDTO>.GeraFalha(conta.Falha!);
        }
        public async Task<Resultado<RetornaContasDTO>> AlterarConta(AtualizaContaDTO contaDTO)
        {
            throw new NotImplementedException();
        }
    }
}
