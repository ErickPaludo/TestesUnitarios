using Financ.Domain.Enums;
using Financ.Domain.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Entidades
{
    public sealed class Contas : BaseConta
    {
        public string? Titulo { get; private set; }
        public TipoConta TipoConta { get; private set; }
        public int DiaFechamento { get; private set; }
        public int DiaVencimento { get; private set; }

        public Contas(string titulo, TipoConta tipoConta, int diaFechamento, int diaVencimento, Status status, DateTime dthrReg)
        {
            ValidaContas(titulo, tipoConta, diaFechamento, diaVencimento, status, dthrReg);
        }
        public Contas(int id, string titulo, TipoConta tipoConta, int diaFechamento, int diaVencimento, Status status, DateTime dthrReg)
        {
            ValidacaoDominio.VerificaExcessao(id <= 0, MensagensDominio.ID_IGUAL_MENOR_ZERO);
            Id = id;
            ValidaContas(titulo, tipoConta, diaFechamento, diaVencimento, status, dthrReg);
        }

        private void ValidaContas(string titulo, TipoConta tipoConta, int diaFechamento, int diaVencimento, Status status, DateTime dthrReg)
        {
            #region Titulo
            ValidacaoDominio.VerificaExcessao(string.IsNullOrEmpty(titulo), MensagensDominio.TITULO_OBRIGATORIO);
            ValidacaoDominio.VerificaExcessao(titulo.Length < 5 || titulo.Length > 100, MensagensDominio.TITULO_TAMANHO_INVALIDO);
            #endregion

            #region Tipo da conta
            ValidacaoDominio.VerificaExcessao(!Enum.IsDefined(typeof(TipoConta), tipoConta), MensagensDominio.TIPO_CONTA_INVALIDO);
            #endregion

            #region Status
            ValidacaoDominio.VerificaExcessao(!Enum.IsDefined(typeof(Status), status), MensagensDominio.STATUS_INVALIDO);
            #endregion

            #region Fechamento/Vencimento
            ValidacaoDominio.VerificaExcessao(diaFechamento < 1 || diaFechamento > 16, MensagensDominio.FECHAMENTO_INVALIDO);

            int diferencaDiasFechamento = diaVencimento - diaFechamento; //diferença entre o dia de fechamento e o dia de vencimento

            ValidacaoDominio.VerificaExcessao(diaVencimento <= diaFechamento, MensagensDominio.VENCIMENTO_MENOR_FECHAMENTO);

            ValidacaoDominio.VerificaExcessao(diferencaDiasFechamento < 7, MensagensDominio.VENCIMENTO_MINIMO_7_DIAS);

            ValidacaoDominio.VerificaExcessao(diferencaDiasFechamento > 12, MensagensDominio.VENCIMENTO_MAXIMO_12_DIAS);
            #endregion

            #region Registro
            ValidacaoDominio.VerificaExcessao(dthrReg.Date != DateTime.Now.Date, MensagensDominio.DATA_REGISTRO_INVALIDA);
            #endregion

            Titulo = titulo;
            TipoConta = tipoConta;
            DiaFechamento = diaFechamento;
            DiaVencimento = diaVencimento;
            Status = status;
            DthrReg = dthrReg;
        }
        //Todo: Implantar todas as variaveis de mensagem, e terminar testes unitarios
    }
}
