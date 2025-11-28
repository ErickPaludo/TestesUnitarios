using Financ.Domain.Entidades;
using Financ.Domain.Interfaces.Autenticação;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Infra.Data.Identity
{
    public class Autenticacao : IAutenticacao
    {
        private readonly UserManager<UsuarioIdentity> _gerenciaUsuarios;
        private readonly SignInManager<UsuarioIdentity> _gerenciaLogin;
        private readonly IConfiguration _configuration;
        public Autenticacao(UserManager<UsuarioIdentity> gerenciaUsuarios, SignInManager<UsuarioIdentity> gerenciaLogin, IConfiguration configuration)
        {
            _gerenciaUsuarios = gerenciaUsuarios;
            _gerenciaLogin = gerenciaLogin;
            _configuration = configuration;
        }
        public async Task<bool> Autenticador(string email, string senha)
        {
            return (await _gerenciaLogin.PasswordSignInAsync(email, senha, false, lockoutOnFailure: false)).Succeeded;
        }
        public async Task<string> ObtemIdUsuario(string email)
        {
            var usuario = await _gerenciaUsuarios.FindByEmailAsync(email);
            if(usuario == null)
              return string.Empty;
            
            return _gerenciaUsuarios.GetUserIdAsync(usuario!).Result;  
        }
        public async Task<(bool,string?)> RegistrarUsuario(Usuario usuario,string senha)
        {
            var usuarioidentity = new UsuarioIdentity
            {
                Email = usuario.Email,
                UserName = usuario.Email,
                PrimeiroNome = usuario.PrimeiroNome,
                SegundoNome = usuario.SegundoNome 
            };
            var usuarioCriado = await _gerenciaUsuarios.CreateAsync(usuarioidentity, senha);
        
            return (usuarioCriado.Succeeded,string.Join("\n", usuarioCriado.Errors.Select(x => x.Description)));
        }
        public (string email, DateTime Expiracao) GeraToken(string id, string email)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gerar chave privada para assinar o token
            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

            //gerar a assinatura digital
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //definir o tempo de expiração
            var expiration = DateTime.UtcNow.AddHours(1);
#if DEBUG
            expiration = DateTime.UtcNow.AddDays(30);
#endif

            //gerar o token
            JwtSecurityToken token = new JwtSecurityToken(
                //emissor
                issuer: _configuration["Jwt:Issuer"],
                //audiencia
                audience: _configuration["Jwt:Audience"],
                //data de expiracao
                expires: expiration,
                //assinatura digital
                signingCredentials: credentials,

                claims: claims
                );

            return (new JwtSecurityTokenHandler().WriteToken(token), expiration);
        }

        public async Task<Usuario> ObtemUsuario(Guid idUsuario)
        {
            var usuario = _gerenciaUsuarios.FindByIdAsync(idUsuario.ToString()).Result;

            return new Usuario(idUsuario,usuario.UserName,usuario.UserName,usuario.Email);
        }
    }
}
