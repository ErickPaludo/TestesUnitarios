using Financ.Application.CQRS.Commands;
using Financ.Application.CQRS.Handler;
using Financ.Application.DTOs.ContasUsuarios;
using Financ.Application.Interfaces.ContasUsuarios;
using Financ.Domain.Entidades;
using Financ.Domain.Interfaces.Repositorios;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Servicos
{
    public class ContasUsuariosServicos : IContasUsuariosServicos
    {
        private readonly IMediator _mediator;
        public ContasUsuariosServicos(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task IncluiUsuarioNaConta(InclusaoContaUsuarioDTO contaUsuarioDTO)
        {
            var commandContaUsuario = new IncluiUsuarioContaCommand(contaUsuarioDTO.IdConta, Guid.NewGuid(), contaUsuarioDTO.Acesso);

            var contaUsuario = await _mediator.Send(commandContaUsuario);
        }
    }
}
