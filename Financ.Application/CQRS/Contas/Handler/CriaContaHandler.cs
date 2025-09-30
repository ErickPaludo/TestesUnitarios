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
    public class CriaContaHandler : IRequestHandler<ContaCommand, int>
    {
        private readonly IContasRepositorio _contasRepositorio;
        public CriaContaHandler(IContasRepositorio contasRepositorio)
        {
            _contasRepositorio = contasRepositorio;
        }
        public async Task<int> Handle(ContaCommand request, CancellationToken cancellationToken)
        {
            Contas contas = new Contas(request.Titulo,request.TipoConta,request.DiaFechamento,request.DiaVencimento,request.CreditoLimite,request.Status,request.DthrReg);
            await _contasRepositorio.Adicionar(contas);
            return contas.Id;
        }
    }
}
