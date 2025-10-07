﻿using Financ.Application.Comun.Resultado;
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
    public class AtualizarContasHandler : IRequestHandler<AtualizarContaCommand, Resultado<Contas>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AtualizarContasHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Resultado<Contas>> Handle(AtualizarContaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contaUsuario = await _unitOfWork.contasUsuariosRepositorio.BuscarObjetoUnico(x => x.Id == request.IdContaUsuario);
                if (contaUsuario == null)
                    return Resultado<Contas>.GeraFalha(Falha.NaoEncontrado("Conta ou usuário inválidos."));

                var conta = await _unitOfWork.contasRepositorio.BuscarPeloId<int>(contaUsuario.IdConta);
                if (conta == null)
                    return Resultado<Contas>.GeraFalha(Falha.NaoEncontrado("Conta não encontrada."));

                conta.AtualizaConta(contaUsuario, request.Titulo, request.Status, request.CreditoLimite, request.DiaFechamento, request.DiaVencimento);

                _unitOfWork.contasRepositorio.Atualiza(conta);
                return Resultado<Contas>.GeraSucesso(conta);
            }
            catch (ContasValidacao contasExecao)
            {
                return Resultado<Contas>.GeraFalha(Falha.ErroOperacional(contasExecao.Message));
            }
        }
    }
}
