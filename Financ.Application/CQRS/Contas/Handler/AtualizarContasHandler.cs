using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Commands;
using Financ.Domain.Entidades;
using Financ.Domain.Enums;
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
                var contaUsuario = await _unitOfWork.contasUsuariosRepositorio.BuscarObjetoUnico(x => x.Id == request.IdConta);
                if (contaUsuario == null)
                    return Resultado<Contas>.GeraFalha(Falha.NaoEncontrado("Conta ou usuário inválidos."));

                contaUsuario = await _unitOfWork.contasUsuariosRepositorio.BuscarObjetoUnico(x => x.IdConta == request.IdConta && x.IdUsuario == request.IdUsuario);

                if (contaUsuario == null)
                    return Resultado<Contas>.GeraFalha(Falha.ErroOperacional("O Usuário não pertence a está conta!"));

                if (!contaUsuario.Status.Equals(TiposStatus.Ativo))
                    return Resultado<Contas>.GeraFalha(Falha.ErroOperacional($"Não foi possível concluir a operação pois seu usuário está {contaUsuario.Status.ToString()}!"));

                if (!contaUsuario.Acesso.Equals(TiposAcessos.Administrador))
                    return Resultado<Contas>.GeraFalha(Falha.ErroOperacional("O Usuário não um adiministrador!"));

                var conta = await _unitOfWork.contasRepositorio.BuscarPeloId<int>(contaUsuario.IdConta);
                if (conta == null)
                    return Resultado<Contas>.GeraFalha(Falha.NaoEncontrado("Conta não encontrada."));

                conta.AtualizaConta(contaUsuario, request.Titulo,request.CreditoAtivo, request.Status, request.CreditoLimite, request.DiaFechamento, request.DiaVencimento);

                _unitOfWork.contasRepositorio.Atualiza(conta);
                await _unitOfWork.Commit();
                return Resultado<Contas>.GeraSucesso(conta);
            }
            catch (ContasValidacao contasExecao)
            {
                return Resultado<Contas>.GeraFalha(Falha.ErroOperacional(contasExecao.Message));
            }
        }
    }
}
