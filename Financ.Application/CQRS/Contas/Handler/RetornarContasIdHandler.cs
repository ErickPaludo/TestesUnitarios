using Financ.Application.CQRS.Querys;
using Financ.Application.DTOs;
using Financ.Domain.Interfaces;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Handler
{
    public class RetornarContasIdHandler : IRequestHandler<RetornarContaIdQuery, RetornaContasDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RetornarContasIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RetornaContasDTO> Handle(RetornarContaIdQuery request, CancellationToken cancellationToken)
        {
            var conta = await _unitOfWork.contasRepositorio.BuscarPeloId(request.IdConta);

            if (conta == null) return null;

            var contaDTO = new RetornaContasDTO
            {
                IdConta = conta.Id,
                Titulo = conta.Titulo,
                CreditoLimite = conta.CreditoLimite,
                DiaFechamento = conta.DiaFechamento,
                DiaVencimento = conta.DiaVencimento
            };

            return contaDTO;
        }
    }
}
