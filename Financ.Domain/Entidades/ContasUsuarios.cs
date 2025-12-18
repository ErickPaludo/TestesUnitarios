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
        public Conta? Contas { get; private set; }

        public ContasUsuarios() { }
        public ContasUsuarios(Conta conta, string idUsuario, TiposAcessos acesso, TiposStatus status)
        {
            ValidaContasUsuarios(conta, idUsuario);
            ContasUsuariosValidacao.Verifica(!Enum.IsDefined(typeof(TiposStatus), status), MensagensBase.STATUS_INVALIDO);
            ContasUsuariosValidacao.Verifica(!Enum.IsDefined(typeof(TiposAcessos), acesso), MensagensContasUsuarios.ACESSO_INVALIDO);
            Status = status;
            Acesso = acesso;
        }
        public ContasUsuarios(Conta conta, string idUsuario)
        {
            ValidaContasUsuarios(conta, idUsuario);
            Status = TiposStatus.Ativo;
            Acesso = TiposAcessos.Mestre;
        }
        public ContasUsuarios(int id, Conta conta, string idUsuario, TiposAcessos acesso, TiposStatus status)
        {
            ValidaContasUsuarios(conta, idUsuario);
            ContasUsuariosValidacao.Verifica(id <= 0, MensagensBase.ID_IGUAL_MENOR_ZERO);
            ContasUsuariosValidacao.Verifica(!Enum.IsDefined(typeof(TiposStatus), status), MensagensBase.STATUS_INVALIDO);
            ContasUsuariosValidacao.Verifica(!Enum.IsDefined(typeof(TiposAcessos), acesso), MensagensContasUsuarios.ACESSO_INVALIDO);
            Id = id;
            Status = status;
            Acesso = acesso;
        }
        private void ValidaContasUsuarios(Conta conta, string idUsuario)
        {
            ContasUsuariosValidacao.Verifica(conta is null, MensagensContasUsuarios.CONTA_NAO_PODE_SER_NULA);
            ContasUsuariosValidacao.Verifica(string.IsNullOrWhiteSpace(idUsuario), MensagensContasUsuarios.IDUSUARIO_VAZIO);          

            Contas = conta;
            IdUsuario = idUsuario;
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
