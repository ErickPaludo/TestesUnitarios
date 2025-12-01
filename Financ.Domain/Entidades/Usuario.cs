using Financ.Domain.Interfaces.InterfaceEntidades;
using Financ.Domain.Validacoes;
using Financ.Domain.Validacoes.Mensagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Entidades
{
    public class Usuario : IUsuarioIdentity
    {
        public Guid IdUsuario { get; private set; }
        public string PrimeiroNome { get; private set; }
        public string SegundoNome { get; private set; }
        public string Email { get; private set; }

        public Usuario(Guid idUsuario, string primeiroNome, string segundoNome, string email)
        {
            UsuariosValidacoes.Verifica(idUsuario == Guid.Empty, MensagensBase.USUARIO_NAO_INFORMADO);
            VerificaNome(primeiroNome, segundoNome);
            VerificaEmail(email);

            IdUsuario = idUsuario;
        }

        public Usuario(string primeiroNome, string segundoNome, string email)
        {
            VerificaNome(primeiroNome, segundoNome);
            VerificaEmail(email);
        }

        private void VerificaNome(string primeiroNome, string segundoNome)
        {
            UsuariosValidacoes.Verifica(string.IsNullOrWhiteSpace(primeiroNome), MensagensUsuarios.PRIMEIRO_NOME_OBRIGATORIO);
            UsuariosValidacoes.Verifica(string.IsNullOrWhiteSpace(segundoNome), MensagensUsuarios.SEGUNDO_NOME_OBRIGATORIO);

            UsuariosValidacoes.Verifica(primeiroNome.Length > 100, MensagensUsuarios.PRIMEIRO_NOME_MAXIMO);
            UsuariosValidacoes.Verifica(primeiroNome.Length < 3, MensagensUsuarios.PRIMEIRO_NOME_MINIMO);

            UsuariosValidacoes.Verifica(segundoNome.Length > 100, MensagensUsuarios.SEGUNDO_NOME_MAXIMO);
            UsuariosValidacoes.Verifica(segundoNome.Length < 3, MensagensUsuarios.SEGUNDO_NOME_MINIMO);

            PrimeiroNome = primeiroNome;
            SegundoNome = segundoNome;
        }  
        private void VerificaEmail(string email)
        {
            UsuariosValidacoes.Verifica(string.IsNullOrWhiteSpace(email), MensagensUsuarios.EMAIL_OBRIGATORIO);
            UsuariosValidacoes.Verifica(email.Length > 256, MensagensUsuarios.EMAIL_MAXIMO);
            UsuariosValidacoes.Verifica(email.Length < 6, MensagensUsuarios.EMAIL_MINIMO);
            Email = email;
        }
    }
}
