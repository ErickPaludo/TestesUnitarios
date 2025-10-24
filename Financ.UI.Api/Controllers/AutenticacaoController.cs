using Financ.Application.Interfaces.Autenticação;
using Financ.Domain.Interfaces.Autenticação;
using Financ.UI.Api.Extensao;
using Financ.UI.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financ.UI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioAutenticacao _usuarioAutenticacao;
        public AutenticacaoController(IUsuarioAutenticacao usuarioAutenticacao)
        {
            _usuarioAutenticacao = usuarioAutenticacao;
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(CadastraUsuarioDTO usuarioDTO)
        {
            var usuario = await _usuarioAutenticacao.CadastraUsuario(usuarioDTO);
            return usuario.RetornoAutomatico();
        }
        [HttpPost]
        public async Task<IActionResult> RetornaToken(ConectaUsuarioDTO usuario)
        {
           // var resultado = await _autenticacao.Autenticador(usuario.Email, usuario.Senha);
         //   return Ok(resultado);
            return Ok();
        }
    }
}
