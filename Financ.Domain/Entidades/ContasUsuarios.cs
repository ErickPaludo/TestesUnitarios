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
        public ContasUsuarios(Contas conta, string idUsuario, TiposAcessos acesso, TiposStatus status)
        {
            ValidaContasUsuarios(conta, idUsuario, acesso, status);
        }
        public ContasUsuarios(int id, Contas conta, string idUsuario, TiposAcessos acesso, TiposStatus status)
        {
            ContasUsuariosValidacao.Verifica(id <= 0, MensagensBase.ID_IGUAL_MENOR_ZERO);
            Id = id;
            ValidaContasUsuarios(conta, idUsuario, acesso, status);
        }
        private void ValidaContasUsuarios(Contas conta, string idUsuario, TiposAcessos acesso, TiposStatus status)
        {
            ContasUsuariosValidacao.Verifica(conta is null, MensagensContasUsuarios.CONTA_NAO_PODE_SER_NULA);
            ContasUsuariosValidacao.Verifica(string.IsNullOrWhiteSpace(idUsuario), MensagensContasUsuarios.IDUSUARIO_VAZIO);
            ContasUsuariosValidacao.Verifica(!Enum.IsDefined(typeof(TiposAcessos), acesso), MensagensContasUsuarios.ACESSO_INVALIDO);
            ContasUsuariosValidacao.Verifica(!Enum.IsDefined(typeof(TiposStatus), status), MensagensBase.STATUS_INVALIDO);

            Contas = conta;
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
