using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Commands;
using Financ.Application.DTOs.Contas.Get;
using Financ.Application.DTOs.ContasUsuarios.Get;
using Financ.Application.Mapeamento;
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
    public class CriarContaHandler : IRequestHandler<CriarContaCommand, Resultado<RetornaContasDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CriarContaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Resultado<RetornaContasDTO>> Handle(CriarContaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Contas conta = new Contas(request.Titulo, request.TipoConta, request.CreditoAtivo, request.DiaFechamento, request.DiaVencimento, request.CreditoLimite, request.CreditoMaximo, request.Status);

                var contaUsuario = new ContasUsuarios(conta, request.IdUsuario, request.Acesso, request.Status);
                conta.ContasUsuarios!.Add(contaUsuario);

                await _unitOfWork.contasRepositorio.Adicionar(conta);
                await _unitOfWork.contasUsuariosRepositorio.Adicionar(contaUsuario);
                await _unitOfWork.Commit();

                return Resultado<RetornaContasDTO>.GeraSucesso(ContaMapper.ParaDTO(conta));
            }
            catch (ContasValidacao contasExecao)
            {
                return Resultado<RetornaContasDTO>.GeraFalha(Falha.ErroOperacional(contasExecao.Message));
            }
            catch (ContasUsuariosValidacao contasUseuariosExcessao)
            {
                return Resultado<RetornaContasDTO>.GeraFalha(Falha.ErroOperacional(contasUseuariosExcessao.Message));
            }
        }
    }
}
