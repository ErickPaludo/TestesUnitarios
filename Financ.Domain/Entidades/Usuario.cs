using Financ.Domain.Interfaces.InterfaceEntidades;
using Financ.Domain.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Entidades
{
    public class Usuario : IUsuario
    {
        public Guid IdUsuario { get; private set; }
        public string PrimeiroNome { get; private set; }
        public string SegundoNome { get; private set; }
        public string Email { get; private set; }

        public Usuario(Guid idUsuario, string primeiroNome, string segundoNome, string email)
        {
            IdUsuario = idUsuario;
            VerificaNome(primeiroNome, segundoNome);
            VerificaEmail(email);
        }

        public Usuario(string primeiroNome, string segundoNome, string email)
        {
            VerificaNome(primeiroNome, segundoNome);
            VerificaEmail(email);
        }

        private void VerificaNome(string primeiroNome, string segundoNome)
        {
            UsuariosValidacoes.Verifica(string.IsNullOrWhiteSpace(primeiroNome), "O primeiro nome do usuário é obrigatório.");
            UsuariosValidacoes.Verifica(string.IsNullOrWhiteSpace(segundoNome), "O Segundo nome do usuário é obrigatório.");

            UsuariosValidacoes.Verifica(primeiroNome.Length > 100, "O primeiro nome não deve possuir mais do que 100 caracteres");
            UsuariosValidacoes.Verifica(primeiroNome.Length < 3, "O primeiro nome não deve possuir menos do que 3 caracteres");

            UsuariosValidacoes.Verifica(segundoNome.Length > 100, "O primeiro nome não deve possuir mais do que 100 caracteres");
            UsuariosValidacoes.Verifica(segundoNome.Length < 3, "O primeiro nome não deve possuir menos do que 3 caracteres");

            PrimeiroNome = primeiroNome;
            SegundoNome = segundoNome;
        }  
        private void VerificaEmail(string email)
        {
            UsuariosValidacoes.Verifica(string.IsNullOrWhiteSpace(email), "O email do usuário é obrigatório.");
            UsuariosValidacoes.Verifica(email.Length > 256, "O email não deve possuir mais do que 256 caracteres");
            UsuariosValidacoes.Verifica(email.Length < 6, "O email não deve possuir menos do que 6 caracteres");
            Email = email;
        }
    }
}
