using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Commands;
using Financ.Application.CQRS.Usuarios.Commands;
using Financ.Application.CQRS.Usuarios.Querys;
using Financ.Application.DTOs.Usuarios.Get;
using Financ.Application.DTOs.Usuarios.Post;
using Financ.Application.Interfaces.Usuarios;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Servicos.Usuarios
{
    public class UsuariosServicos : IUsuariosServicos
    {
        private readonly IMediator _mediator;
        public UsuariosServicos(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Resultado<string>> CadastraUsuario(CadastraUsuarioDTO usuarioDTO)
        {
            return await _mediator.Send(new CadastraUsuarioCommand(usuarioDTO.Email, usuarioDTO.PrimeiroNome, usuarioDTO.SegundoNome, usuarioDTO.Senha, usuarioDTO.ConfirmarSenha));
        }

        public async Task<Resultado<RetornaUsuarioDTO>> RetornaUsuario(Guid idUsuario)
        {
            return await _mediator.Send(new RetornaUsuarioPorIdQuery(idUsuario));
        }
    }
}
