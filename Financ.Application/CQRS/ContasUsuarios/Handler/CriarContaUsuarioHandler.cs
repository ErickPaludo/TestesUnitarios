using Financ.Application.Comun.Resultado;
using Financ.Application.Comun.Resultadoado;
using Financ.Application.CQRS.Commands;
using Financ.Domain.Entidades;
using Financ.Domain.Interfaces;
using Financ.Domain.Interfaces.Repositorios;
using Financ.Domain.Validacoes;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Handler
{
    public class CriarContaUsuarioHandler : IRequestHandler<CriarContaUsuarioCommand, Resultado<ContasUsuarios>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CriarContaUsuarioHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Resultado<ContasUsuarios>> Handle(CriarContaUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contaUsuario = new ContasUsuarios(request.IdConta, request.IdUsuario, request.Acesso, request.Status);
                contaUsuario = await _unitOfWork.contasUsuariosRepositorio.Adicionar(contaUsuario);
                await _unitOfWork.Commit();
                return Resultado<ContasUsuarios>.GeraSucesso(contaUsuario);
            }
            catch (ContasUsuariosValidacao contasUseuariosExcessao)
            {
                return Resultado<ContasUsuarios>.GeraFalha(Falha.ErroOperacional(contasUseuariosExcessao.Message));
            }
        }
    }
}
