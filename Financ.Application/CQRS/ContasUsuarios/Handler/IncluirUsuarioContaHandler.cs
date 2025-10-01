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
    public class IncluirUsuarioContaHandler : IRequestHandler<IncluiUsuarioContaCommand, ContasUsuarios>
    {
        private readonly IUnitOfWork _unitOfWork;
        public IncluirUsuarioContaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ContasUsuarios> Handle(IncluiUsuarioContaCommand request, CancellationToken cancellationToken)
        {
            //if (_unitOfWork.contasRepositorio.BuscarId(request.IdConta))
            //{
            var contaUsuario = new ContasUsuarios(request.IdConta, request.IdUsuario, request.Acesso, request.Status, request.DthrReg);
            contaUsuario = await _unitOfWork.contasUsuariosRepositorio.Adicionar(contaUsuario);
            await _unitOfWork.Commit();
            return contaUsuario;
            //}
        }
    }
}
