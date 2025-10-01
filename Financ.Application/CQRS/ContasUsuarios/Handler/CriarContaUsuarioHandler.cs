using Financ.Application.CQRS.Commands;
using Financ.Domain.Entidades;
using Financ.Domain.Interfaces;
using Financ.Domain.Interfaces.Repositorios;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Handler
{
    public class CriarContaUsuarioHandler : IRequestHandler<CriarContaUsuarioCommand, ContasUsuarios>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CriarContaUsuarioHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ContasUsuarios> Handle(CriarContaUsuarioCommand request, CancellationToken cancellationToken)
        {
            var contaUsuario = new ContasUsuarios(request.IdConta, request.IdUsuario, request.Acesso, request.Status, request.DthrReg);
            contaUsuario = await _unitOfWork.contasUsuariosRepositorio.Adicionar(contaUsuario);
            await _unitOfWork.Commit();
            return contaUsuario;
        }
    }
}
