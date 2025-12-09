using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Commands;
using Financ.Application.CQRS.Handler;
using Financ.Application.CQRS.Querys;
using Financ.Application.DTOs.ContasUsuarios.Get;
using Financ.Application.DTOs.ContasUsuarios.Get.Filtros;
using Financ.Application.DTOs.ContasUsuarios.Patch;
using Financ.Application.DTOs.ContasUsuarios.Post;
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

        public async Task<Resultado<RetornaCadastroContasUsuariosDTO>> IncluiUsuarioNaConta(InclusaoContaUsuarioDTO contaUsuarioDTO, Guid idUsuario)
        {
            var commandContaUsuario = new IncluiUsuarioContaCommand(contaUsuarioDTO.IdConta, idUsuario, contaUsuarioDTO.Acesso);

            var contaUsuario = await _mediator.Send(commandContaUsuario);

            return contaUsuario;
        }
        public async Task<Resultado<RetornaCadastroContasUsuariosDTO>> AtualizaUsuarioConta(Guid idUsuario, int idConta, AtualizaContasUsuariosDTO contaUsuario)
        {
            return await _mediator.Send(new AtualizarContaUsuarioCommand(idUsuario, idConta, contaUsuario.Acesso,contaUsuario.Status));
        }


        public async Task<Resultado<List<RetornaUsuariosAssociadosDTO>>> RetornaUsuariosAssociados(FiltroUsuarioAssociado filtroConta, Guid idUsuarioSolicitante)
        {
           return await _mediator.Send(new RetornaUsuariosAssociadosQuery(idUsuarioSolicitante,filtroConta.IdConta,filtroConta.IdUsuario, filtroConta.NomeUsuario,filtroConta.Acesso,filtroConta.Status));
        }
    }
}
