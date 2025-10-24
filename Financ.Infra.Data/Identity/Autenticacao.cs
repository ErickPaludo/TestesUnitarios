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
        private readonly UserManager<UsuarioIdentity> _gerenciaUsuarios;
        private readonly SignInManager<UsuarioIdentity> _gerenciaLogin;
        public Autenticacao(UserManager<UsuarioIdentity> gerenciaUsuarios, SignInManager<UsuarioIdentity> gerenciaLogin)
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
            var usuario = new UsuarioIdentity
            {
                Email = email,
                UserName = email
            };
            var usuarioCriado = await _gerenciaUsuarios.CreateAsync(usuario, senha);

            return usuarioCriado.Succeeded;
        }
    }
}
