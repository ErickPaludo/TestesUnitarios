using Financ.Domain.Enums;
using Financ.Domain.Validacoes.Mensagens;
using Financ.Domain.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Entidades
{
    public sealed class Convites
    {
        public int Id { get; private set; }
        public string IdUsuarioRemetente { get; private set; }
        public string IdUsuarioDestinatario { get; private set; }
        public int IdConta { get; private set; }
        public TiposAcessos Acesso { get; private set; }
        public bool? Aceito { get; private set; }
        public DateTime Expiracao { get; private set; }

        public Convites(int id, string idUsuarioRemetente, string idUsuarioDestinatario, int idConta, TiposAcessos acesso, DateTime expiracao)
        {
            ConvitesValidacao.Verifica(id <= 0, MensagensBase.ID_IGUAL_MENOR_ZERO);
            Id = id;
            ValidaUsuarios(idUsuarioRemetente, idUsuarioDestinatario);
            ConvitesValidacao.Verifica(idConta == 0, MensagensContas.ID_NAO_PODE_SER_IGUAL_A_ZERO);
            ConvitesValidacao.Verifica(!Enum.IsDefined(typeof(TiposAcessos), acesso), MensagensContasUsuarios.ACESSO_INVALIDO);
            IdConta = idConta;
            Acesso = acesso;
            Expiracao = DateTime.Now.AddDays(7);
        }

        public Convites(string idUsuarioRemetente, string idUsuarioDestinatario, int idConta, TiposAcessos acesso, DateTime expiracao)
        {
            ValidaUsuarios(idUsuarioRemetente, idUsuarioDestinatario);
            ConvitesValidacao.Verifica(idConta == 0, MensagensContas.ID_NAO_PODE_SER_IGUAL_A_ZERO);
            ConvitesValidacao.Verifica(!Enum.IsDefined(typeof(TiposAcessos), acesso), MensagensContasUsuarios.ACESSO_INVALIDO);
            IdConta = idConta;
            Acesso = acesso;
            Expiracao = DateTime.Now.AddDays(7);
        }
        private void ValidaUsuarios(string idUsuarioRemetente, string idUsuarioDestinatario)
        {
            ConvitesValidacao.Verifica(string.IsNullOrWhiteSpace(idUsuarioRemetente), MensagensConvite.USUARIO_REMETENTE_INVALIDO);
            ConvitesValidacao.Verifica(string.IsNullOrWhiteSpace(idUsuarioDestinatario), MensagensConvite.USUARIO_DESTINATARIO_INVALIDO);
            IdUsuarioRemetente = idUsuarioRemetente;
            IdUsuarioDestinatario = idUsuarioDestinatario;
        }
    }
}
