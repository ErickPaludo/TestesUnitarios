using Financ.Application.DTOs.Usuarios.Post;
using Financ.Application.Interfaces.Autenticação;
using Financ.Application.Interfaces.Usuarios;
using Financ.Application.Servicos.Usuarios;
using Financ.Domain.Interfaces.Autenticação;
using Financ.UI.Api.Extensao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financ.UI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosServicos _usuarioServicos;

        public UsuariosController(IUsuariosServicos usuarioServicos)
        {
            _usuarioServicos = usuarioServicos;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(CadastraUsuarioDTO usuarioDTO)
        {
            var usuario = await _usuarioServicos.CadastraUsuario(usuarioDTO);
            return usuario.RetornoAutomatico();
        }
        [HttpGet("meus_dados")]
        [Authorize]
        public async Task<IActionResult> MeusDados()
        {
            var usuario = await _usuarioServicos.RetornaUsuario(User.RetornaIdUsuario());
            return usuario.RetornoAutomatico();
        }
    }
}
