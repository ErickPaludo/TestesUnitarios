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
    public class RetornaContasHandler : IRequestHandler<RetornaContaQuery, Resultado<List<RetornaContasDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RetornaContasHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Resultado<List<RetornaContasDTO>>> Handle(RetornaContaQuery request, CancellationToken cancellationToken)
        {
            var filtroId = request.Filtros?.Id;
            var filtroTitulo = request.Filtros?.Titulo;
            var filtroStatus = request.Filtros?.Status;
            var possuiFiltros = request.Filtros != null;

            var contasUsuario = await _unitOfWork.contasUsuariosRepositorio.ObterContasDoUsuario(
                x => x.IdUsuario == request.IdUsuario
                && (!possuiFiltros || (
                    (!filtroId.HasValue || x.IdConta == filtroId.Value) &&
                    (string.IsNullOrEmpty(filtroTitulo) || x.Contas!.Titulo!.Contains(filtroTitulo)) && 
                    (!filtroStatus.HasValue || x.Contas!.Status == filtroStatus.Value)))
            );



            if (contasUsuario.Count() == 0)
                return Resultado<List<RetornaContasDTO>>.GeraFalha(Falha.NaoEncontrado("Nenhuma conta foi encontrada!"));

            List<RetornaContasDTO> listaContas = new List<RetornaContasDTO>();
            foreach (var conta in contasUsuario)
            {
                if(conta.Contas == null)
                    return Resultado<List<RetornaContasDTO>>.GeraFalha(Falha.NaoEncontrado("Conta não encontrada!"));

                listaContas.Add(new RetornaContasDTO
                {
                    IdConta = conta.Contas.Id,
                    Titulo = conta.Contas.Titulo,
                    CreditoAtivo = conta.Contas.CreditoAtivo,
                    DiaFechamento = conta.Contas.DiaFechamento,
                    DiaVencimento = conta.Contas.DiaVencimento,
                    CreditoMaximo = conta.Contas.CreditoMaximo,
                });
            }

            return Resultado<List<RetornaContasDTO>>.GeraSucesso(listaContas);
        }
    }
}
