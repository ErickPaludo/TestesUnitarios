using Financ.Domain.Enums;
using Financ.Domain.Validacoes;
using Financ.Domain.Validacoes.Mensagens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Entidades
{
    public sealed class ContasUsuarios : BaseConta
    {
        public int IdConta { get; private set; }
        public string IdUsuario { get; private set; }
        public TiposAcessos Acesso { get; private set; }
        public Contas? Contas { get; private set; }

        public ContasUsuarios() { }
        public ContasUsuarios(int idConta, string idUsuario, TiposAcessos acesso, TiposStatus status)
        {
            ValidaContasUsuarios(idConta, idUsuario, acesso, status);
        }
        public ContasUsuarios(int id, int idConta, string idUsuario, TiposAcessos acesso, TiposStatus status)
        {
            ContasUsuariosValidacao.Verifica(id <= 0, MensagensBase.ID_IGUAL_MENOR_ZERO);
            Id = id;
            ValidaContasUsuarios(idConta, idUsuario, acesso, status);
        }
        private void ValidaContasUsuarios(int idConta, string idUsuario, TiposAcessos acesso, TiposStatus status)
        {
            ContasUsuariosValidacao.Verifica(idConta <= 0, MensagensContasUsuarios.IDCONTA_IGUAL_MENOR_ZERO);
            ContasUsuariosValidacao.Verifica(string.IsNullOrWhiteSpace(idUsuario), MensagensContasUsuarios.IDUSUARIO_VAZIO);
            ContasUsuariosValidacao.Verifica(!Enum.IsDefined(typeof(TiposAcessos), acesso), MensagensContasUsuarios.ACESSO_INVALIDO);
            ContasUsuariosValidacao.Verifica(!Enum.IsDefined(typeof(TiposStatus), status), MensagensBase.STATUS_INVALIDO);

            IdConta = idConta;
            IdUsuario = idUsuario;
            Acesso = acesso;
            Status = status;
            DthrReg = DateTime.Now;
        }
        public void AtualizaContasUsuario(TiposAcessos? acessos, TiposStatus? status)
        {
            ContasUsuariosValidacao.Verifica(Acesso == TiposAcessos.Mestre, MensagensContasUsuarios.USUARIO_MESTRE_NAO_PODE_SER_ATUALIZADO);

            if (acessos.HasValue)
            {
                ContasUsuariosValidacao.Verifica(!Enum.IsDefined(typeof(TiposAcessos), acessos.Value), MensagensContasUsuarios.ACESSO_INVALIDO);
                Acesso = acessos.Value;
            }
            if (status.HasValue)
            {
                ContasUsuariosValidacao.Verifica(!Enum.IsDefined(typeof(TiposStatus), status.Value), MensagensBase.STATUS_INVALIDO);
                Status = status.Value;
            }
        }

    }
}
