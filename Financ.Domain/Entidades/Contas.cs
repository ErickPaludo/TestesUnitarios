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
            ValidacaoDominio.VerificaExcessao(id <= 0, "Id inválido!");
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
            ValidacaoDominio.VerificaExcessao(diaFechamento < 1 || diaFechamento > 25, "Dia de fechamento inválido, deve estar entre dia 1 e 25.");

            int diferencaDiasFechamento = diaVencimento - diaFechamento; //diferença entre o dia de fechamento e o dia de vencimento

            ValidacaoDominio.VerificaExcessao(diaVencimento <= diaFechamento, "O vencimento da fatura deve ser maior do que a data de fechamento.");
            ValidacaoDominio.VerificaExcessao(diferencaDiasFechamento < 7, "O vencimento da fatura deve ser maior do que a data de fechamento.");
            ValidacaoDominio.VerificaExcessao(diferencaDiasFechamento > 12, "O vencimento deve ser de no máximo 12 dias após o fechamento.");
            #endregion

            #region Registro
            ValidacaoDominio.VerificaExcessao(dthrReg.Date != DateTime.Now.Date, "Deve ser registrada a data atual, esta não pode ser manipulada");
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
