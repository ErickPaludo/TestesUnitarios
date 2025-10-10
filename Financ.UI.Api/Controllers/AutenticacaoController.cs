using Financ.Domain.Interfaces.Autenticação;
using Financ.UI.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financ.UI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacao _autenticacao;
        public AutenticacaoController(IAutenticacao autenticacao)
        {
            _autenticacao = autenticacao;
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegisterModel usuario)
        {
            var resultado = await _autenticacao.RegistrarUsuario(usuario.Email, usuario.Password);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> RetornaToken(LoginModel usuario)
        {
            var resultado = await _autenticacao.Autenticador(usuario.Email, usuario.Password);
            return Ok(resultado);
        }
    }
}
