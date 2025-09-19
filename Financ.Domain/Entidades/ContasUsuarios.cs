using Financ.Domain.Enums;
using Financ.Domain.Validacoes;
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
        public int IdUsuario { get; private set; }
        public TiposAcessos Acesso { get; private set; }
        public Contas Contas { get; private set; }

        private ContasUsuarios() { }
        public ContasUsuarios(int idConta,int idUsuario,TiposAcessos acesso,TiposStatus status,DateTime dthReg)
        {
            ValidaContasUsuarios(idConta, idUsuario, acesso, status, dthReg);
        }  
        public ContasUsuarios(int id,int idConta,int idUsuario,TiposAcessos acesso,TiposStatus status,DateTime dthReg)
        {
            ValidacaoDominio.VerificaExcessao(id <= 0, MensagensDominio.ID_IGUAL_MENOR_ZERO);
            Id = id;
            ValidaContasUsuarios(idConta, idUsuario, acesso, status, dthReg);
        }
        private void ValidaContasUsuarios(int idConta, int idUsuario, TiposAcessos acesso, TiposStatus status, DateTime dthrReg)
        {
            ValidacaoDominio.VerificaExcessao(idConta <= 0,MensagensDominio.IDCONTA_IGUAL_MENOR_ZERO);
            ValidacaoDominio.VerificaExcessao(idUsuario <= 0,MensagensDominio.IDUSUARIO_IGUAL_MENOR_ZERO);
            ValidacaoDominio.VerificaExcessao(!Enum.IsDefined(typeof(TiposAcessos),acesso),MensagensDominio.ACESSO_INVALIDO);
            ValidacaoDominio.VerificaExcessao(!Enum.IsDefined(typeof(TiposStatus), status), MensagensDominio.STATUS_INVALIDO);
            ValidacaoDominio.VerificaExcessao(dthrReg.Date != DateTime.Now.Date, MensagensDominio.DATA_REGISTRO_INVALIDA);

            IdConta = idConta;
            IdUsuario = idUsuario;
            Acesso = acesso;
            Status = status;
            DthrReg = dthrReg;
        }
    }
}
