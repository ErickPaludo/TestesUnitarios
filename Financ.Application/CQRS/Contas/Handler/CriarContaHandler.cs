using Financ.Application.CQRS.Commands;
using Financ.Domain.Entidades;
using Financ.Domain.Interfaces;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Handler
{
    public class CriarContaHandler : IRequestHandler<CriarContaCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CriarContaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CriarContaCommand request, CancellationToken cancellationToken)
        {
            Contas contas = new Contas(request.Titulo,request.TipoConta,request.DiaFechamento,request.DiaVencimento,request.CreditoLimite,request.Status,request.DthrReg);
            await _unitOfWork.contasRepositorio.Adicionar(contas);
            await _unitOfWork.Commit();
            return contas.Id;
        }
    }
}
