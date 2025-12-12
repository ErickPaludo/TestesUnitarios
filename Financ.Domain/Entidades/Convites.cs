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

        public Convites(ContasUsuarios usuarioRemetente, string idUsuarioDestinatario, Contas conta, TiposAcessos acesso)
        {
            ConvitesValidacao.Verifica(usuarioRemetente.Acesso != TiposAcessos.Mestre, MensagensConvite.USUARIO_SEM_PERMISSAO);
            ConvitesValidacao.Verifica(conta.Status != TiposStatus.Ativo, MensagensConvite.USUARIO_SEM_PERMISSAO);
            ValidaUsuarios(usuarioRemetente.IdUsuario, idUsuarioDestinatario);
            IdConta = conta.Id;
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
