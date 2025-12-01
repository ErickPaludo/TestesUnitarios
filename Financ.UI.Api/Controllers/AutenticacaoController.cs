using Financ.Application.DTOs.Autenticação;
using Financ.Application.Interfaces.Autenticação;
using Financ.Domain.Interfaces.Autenticação;
using Financ.UI.Api.Extensao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financ.UI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly Application.Interfaces.Autenticação.IAutenticacaoServicos _usuarioAutenticacao;
        public AutenticacaoController(Application.Interfaces.Autenticação.IAutenticacaoServicos usuarioAutenticacao)
        {
            _usuarioAutenticacao = usuarioAutenticacao;
        }
       
        [HttpPost]
        public async Task<IActionResult> RetornaToken(ConectaUsuarioDTO usuario)
        {
            var resultado = await _usuarioAutenticacao.AutenticacaoUsuario(usuario);
            return resultado.RetornoAutomatico();
        }
    }
}
