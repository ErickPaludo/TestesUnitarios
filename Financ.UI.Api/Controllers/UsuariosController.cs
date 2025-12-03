using Financ.Application.DTOs.Usuarios.Post;
using Financ.Application.Interfaces.Autenticação;
using Financ.Application.Interfaces.Usuarios;
using Financ.UI.Api.Extensao;
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
    }
}
