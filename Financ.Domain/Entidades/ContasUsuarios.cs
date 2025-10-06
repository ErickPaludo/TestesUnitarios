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
        public Guid IdUsuario { get; private set; }
        public TiposAcessos Acesso { get; private set; }
        public Contas? Contas { get; private set; }

        public ContasUsuarios() { }
        public ContasUsuarios(int idConta, Guid idUsuario,TiposAcessos acesso,TiposStatus status)
        {
            ValidaContasUsuarios(idConta, idUsuario, acesso, status);
        }  
        public ContasUsuarios(int id,int idConta, Guid idUsuario,TiposAcessos acesso,TiposStatus status )
        {
            ContasUsuariosValidacao.Verifica(id <= 0, MensagensBase.ID_IGUAL_MENOR_ZERO);
            Id = id;
            ValidaContasUsuarios(idConta, idUsuario, acesso, status);
        }
        private void ValidaContasUsuarios(int idConta, Guid idUsuario, TiposAcessos acesso, TiposStatus status)
        {
            ContasUsuariosValidacao.Verifica(idConta <= 0, MensagensContasUsuarios.IDCONTA_IGUAL_MENOR_ZERO);
            ContasUsuariosValidacao.Verifica(idUsuario == Guid.Empty, MensagensContasUsuarios.IDUSUARIO_VAZIO);
            ContasUsuariosValidacao.Verifica(!Enum.IsDefined(typeof(TiposAcessos),acesso),MensagensContasUsuarios.ACESSO_INVALIDO);
            ContasUsuariosValidacao.Verifica(!Enum.IsDefined(typeof(TiposStatus), status), MensagensContas.STATUS_INVALIDO);

            IdConta = idConta;
            IdUsuario = idUsuario;
            Acesso = acesso;
            Status = status;
            DthrReg = DateTime.Now;
        }
    }
}
