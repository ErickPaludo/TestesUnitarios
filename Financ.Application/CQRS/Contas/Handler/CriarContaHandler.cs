using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Commands;
using Financ.Domain.Entidades;
using Financ.Domain.Interfaces;
using Financ.Domain.Validacoes;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Handler
{
    public class CriarContaHandler : IRequestHandler<CriarContaCommand, Resultado<Contas>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CriarContaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Resultado<Contas>> Handle(CriarContaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Contas contas = new Contas(request.Titulo, request.TipoConta, request.CreditoAtivo, request.DiaFechamento, request.DiaVencimento, request.CreditoLimite, request.CreditoMaximo, request.Status);
                await _unitOfWork.contasRepositorio.Adicionar(contas);
                await _unitOfWork.Commit();
                return Resultado<Contas>.GeraSucesso(contas);
            }
            catch (ContasValidacao contasExecao)
            {
                return Resultado<Contas>.GeraFalha(Falha.ErroOperacional(contasExecao.Message));
            }
        }
    }
}
