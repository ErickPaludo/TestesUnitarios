using Financ.Domain.Enums;
using Financ.Domain.Validacoes;
using Financ.Domain.Validacoes.Mensagens;
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
        public TiposContas TipoConta { get; private set; }
        public int DiaFechamento { get; private set; }
        public int DiaVencimento { get; private set; }
        public double CreditoLimite { get; private set; }

        private Contas() { }
        public ICollection<ContasUsuarios>? ContasUsuarios { get; set; }
        public Contas(string? titulo, TiposContas tipoConta, int diaFechamento, int diaVencimento, double creditoLimite, TiposStatus status)
        {
            ValidaContas(titulo, tipoConta, diaFechamento, diaVencimento, creditoLimite, status);
            DthrReg = DateTime.Now;
        }
        public Contas(int id, string? titulo, TiposContas tipoConta, int diaFechamento, int diaVencimento, double creditoLimite, TiposStatus status)
        {
            ContasValidacao.Verifica(id <= 0, MensagensBase.ID_IGUAL_MENOR_ZERO);
            Id = id;
            DthrReg = DateTime.Now;
            ValidaContas(titulo, tipoConta, diaFechamento, diaVencimento, creditoLimite, status);
        }
        private void ValidaContas(string? titulo, TiposContas tipoConta, int diaFechamento, int diaVencimento, double creditoLimite, TiposStatus status)
        {
            ValidaTitulo(titulo);
            ValidaTipoDaConta(tipoConta);
            ValidaStatus(status);
            ValidaFechamentoVencimento(diaFechamento, diaVencimento);
            ValidaCreditoLimite(creditoLimite);
        }
        private void ValidaTitulo(string? titulo)
        {
            ContasValidacao.Verifica(string.IsNullOrWhiteSpace(titulo), MensagensContas.TITULO_OBRIGATORIO);
            ContasValidacao.Verifica(string.IsNullOrWhiteSpace(titulo) || titulo.Length < 5 || titulo.Length > 100, MensagensContas.TITULO_TAMANHO_INVALIDO);
            Titulo = titulo;
        }
        private void ValidaTipoDaConta(TiposContas tipoConta)
        {
            ContasValidacao.Verifica(!Enum.IsDefined(typeof(TiposContas), tipoConta), MensagensContas.TIPO_CONTA_INVALIDO);
            TipoConta = tipoConta;
        }
        private void ValidaStatus(TiposStatus status)
        {
            ContasValidacao.Verifica(!Enum.IsDefined(typeof(TiposStatus), status), MensagensContas.STATUS_INVALIDO);
            Status = status;
        }
        private void ValidaFechamentoVencimento(int diaFechamento, int diaVencimento)
        {
            ContasValidacao.Verifica(diaFechamento < 1 || diaFechamento > 16, MensagensContas.FECHAMENTO_INVALIDO);

            int diferencaDiasFechamento = diaVencimento - diaFechamento; //diferença entre o dia de fechamento e o dia de vencimento

            ContasValidacao.Verifica(diaVencimento <= diaFechamento, MensagensContas.VENCIMENTO_MENOR_FECHAMENTO);

            ContasValidacao.Verifica(diferencaDiasFechamento < 7, MensagensContas.VENCIMENTO_MINIMO_7_DIAS);

            ContasValidacao.Verifica(diferencaDiasFechamento >= 12, MensagensContas.VENCIMENTO_MAXIMO_12_DIAS);
            DiaFechamento = diaFechamento;
            DiaVencimento = diaVencimento;
        }
        private void ValidaCreditoLimite(double creditoLimite)
        {
            ContasValidacao.Verifica(creditoLimite < 0, MensagensContas.CREDITO_MENOR_QUE_ZERO);
            CreditoLimite = creditoLimite;
        }
        public void AtualizaConta(ContasUsuarios usuarios,string? titulo, TiposStatus status, double creditoLimite, int diaFechamento, int diaVencimento)
        {
            ContasValidacao.Verifica(usuarios == null, MensagensBase.USUARIO_NAO_INFORMADO);
            ContasValidacao.Verifica(usuarios!.Acesso != TiposAcessos.Administrador, MensagensContas.ATUALIZA_CONTA_USUARIO_SEM_PERMISSAO);

            ValidaTitulo(titulo);
            ValidaCreditoLimite(creditoLimite);
            ValidaFechamentoVencimento(diaFechamento, diaVencimento);
            ValidaStatus(status);
        }
    }
}
