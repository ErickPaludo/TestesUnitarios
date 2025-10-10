using Financ.Domain.Interfaces.Autenticação;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Infra.Data.Identity
{
    public class Autenticacao : IAutenticacao
    {
        private readonly UserManager<ApplicationUser> _gerenciaUsuarios;
        private readonly SignInManager<ApplicationUser> _gerenciaLogin;
        public Autenticacao(UserManager<ApplicationUser> gerenciaUsuarios, SignInManager<ApplicationUser> gerenciaLogin)
        {
            _gerenciaUsuarios = gerenciaUsuarios;
            _gerenciaLogin = gerenciaLogin;
        }
        public async Task<bool> Autenticador(string email, string senha)
        {
            return (await _gerenciaLogin.PasswordSignInAsync(email, senha, false, lockoutOnFailure: false)).Succeeded;
        }
        public async Task<bool> RegistrarUsuario(string email, string senha)
        {
            var usuario = new ApplicationUser
            {
                Email = email,
                UserName = email
            };
            var usuarioCriado = await _gerenciaUsuarios.CreateAsync(usuario, senha);

            return usuarioCriado.Succeeded;
        }
        public async Task DeslogaUsuario()
        {
            await _gerenciaLogin.SignOutAsync();
        }

        public async Task<string> ReAutentica()
        {
            var byteChave = new byte[128];
            using var numeroGeradoAleatoriamente = RandomNumberGenerator.Create();

            numeroGeradoAleatoriamente.GetBytes(byteChave);

            var novoToken = Convert.ToBase64String(byteChave);
            return novoToken;
        }
    }
}
