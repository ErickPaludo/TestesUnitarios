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
        public Guid IdUsuarioRemetente { get; private set; }
        public Guid IdUsuarioDestinatario { get; private set; }
        public int IdConta { get; private set; }
        public TiposAcessos Acesso { get; private set; }
        public DateTime Expiracao { get; private set; }

        public Convites(int id, Guid idUsuarioRemetente, Guid idUsuarioDestinatario, int idConta, TiposAcessos acesso, DateTime expiracao)
        {
            ConvitesValidacao.Verifica(id <= 0, MensagensBase.ID_IGUAL_MENOR_ZERO);
            Id = id;
            IdUsuarioRemetente = idUsuarioRemetente;
            IdUsuarioDestinatario = idUsuarioDestinatario;
            IdConta = idConta;
            Acesso = acesso;
            Expiracao = expiracao;
        }

        public Convites(Guid idUsuarioRemetente, Guid idUsuarioDestinatario, int idConta, TiposAcessos acesso, DateTime expiracao)
        {
            IdUsuarioRemetente = idUsuarioRemetente;
            IdUsuarioDestinatario = idUsuarioDestinatario;
            IdConta = idConta;
            Acesso = acesso;
            Expiracao = expiracao;
        }
        private void ValidaUsuarios(Guid idUsuarioRemetente, Guid idUsuarioDestinatario)
        {
            ConvitesValidacao.Verifica(idUsuarioRemetente == Guid.Empty, MensagensConvite.USUARIO_REMETENTE_INVALIDO);
            ConvitesValidacao.Verifica(idUsuarioDestinatario == Guid.Empty, MensagensConvite.USUARIO_DESTINATARIO_INVALIDO);
            IdUsuarioRemetente = idUsuarioRemetente;
            IdUsuarioDestinatario = idUsuarioDestinatario;
        }
    }
}
