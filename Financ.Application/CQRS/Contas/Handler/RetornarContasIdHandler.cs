using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Querys;
using Financ.Application.DTOs;
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
    public class RetornarContasIdHandler : IRequestHandler<RetornarContaIdQuery, Resultado<Contas>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RetornarContasIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Resultado<Contas>> Handle(RetornarContaIdQuery request, CancellationToken cancellationToken)
        {
            var conta = await _unitOfWork.contasRepositorio.BuscarPeloId(request.IdConta);

            return conta == null ? Resultado<Contas>.GeraFalha(Falha.NaoEncontrado("Conta não cadastrada!")) : Resultado<Contas>.GeraSucesso(conta);
        }
    }
}
