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
    public class IncluirUsuarioContaHandler : IRequestHandler<IncluiUsuarioContaCommand, Resultado<ContasUsuarios>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public IncluirUsuarioContaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Resultado<ContasUsuarios>> Handle(IncluiUsuarioContaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _unitOfWork.contasRepositorio.ExisteId(x => x.Id == request.IdConta))
                    return Resultado<ContasUsuarios>.GeraFalha(Falha.NaoEncontrado("Conta não cadastrada!"));

                if ((await _unitOfWork.contasUsuariosRepositorio.ObterContasDoUsuario(x => x.IdUsuario == request.IdUsuario && x.IdConta == request.IdConta)).Count() > 0)
                    return Resultado<ContasUsuarios>.GeraFalha(Falha.ErroOperacional("Usuário já está cadastrado nesta conta!"));

                var contaUsuario = new ContasUsuarios(request.IdConta, request.IdUsuario, request.Acesso, request.Status);
                contaUsuario = await _unitOfWork.contasUsuariosRepositorio.Adicionar(contaUsuario);
                await _unitOfWork.Commit();
                return Resultado<ContasUsuarios>.GeraSucesso(contaUsuario);
            }
            catch (ContasUsuariosValidacao contasUsuariosExcessao)
            {
                return Resultado<ContasUsuarios>.GeraFalha(Falha.ErroOperacional(contasUsuariosExcessao.Message));
            }
            //}
        }
    }
}
