using Financ.Application.CQRS.Commands;
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
        public async Task CriarConta(ContasDTO contaDTO)
        {
            var commandConta = new ContaCommand(contaDTO.Titulo,contaDTO.DiaFechamento,contaDTO.DiaVencimento,contaDTO.LimiteCredito);
           await _mediator.Send(commandConta);
        }
    }
}
