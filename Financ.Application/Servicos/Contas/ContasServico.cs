using Financ.Application.CQRS.Commands;
using Financ.Application.CQRS.Querys;
using Financ.Application.DTOs;
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
        public async Task CriarConta(CadastrarContasDTO contaDTO)
        {
            var commandConta = new CriarContaCommand(contaDTO.Titulo,contaDTO.DiaFechamento,contaDTO.DiaVencimento,contaDTO.CreditoLimite);
            var idConta = await _mediator.Send(commandConta);

            var commandContaUsuario = new CriarContaUsuarioCommand(idConta,Guid.NewGuid());
            await _mediator.Send(commandContaUsuario);
        }

        public async Task<RetornaContasDTO> RetornarContas(int idContas)
        {
            var queryContas = new RetornarContaIdQuery(idContas);
            return await _mediator.Send(queryContas);
        }
    }
}
