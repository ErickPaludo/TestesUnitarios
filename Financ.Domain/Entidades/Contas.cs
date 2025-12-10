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
        public string Titulo { get; private set; }
        public TiposContas TipoConta { get; private set; }
        public bool CreditoAtivo { get; private set; }
        public bool CreditoLimite { get; set; }
        public double? CreditoMaximo { get; private set; }
        public int? DiaFechamento { get; private set; }
        public int? DiaVencimento { get; private set; }

        private Contas() { }
        public ICollection<ContasUsuarios>? ContasUsuarios { get; set; }
        public Contas(string? titulo, TiposContas tipoConta, bool creditoAtivo, int? diaFechamento, int? diaVencimento, bool creditoLimite, double? creditoMaximo, TiposStatus status)
        {
            ValidaContas(titulo, tipoConta, creditoAtivo, diaFechamento, diaVencimento, creditoLimite, creditoMaximo, status);
            DthrReg = DateTime.Now;
        }
        public Contas(int id, string titulo, TiposContas tipoConta, bool creditoAtivo, int? diaFechamento, int? diaVencimento, bool creditoLimite, double? creditoMaximo, TiposStatus status)
        {
            ContasValidacao.Verifica(id <= 0, MensagensBase.ID_IGUAL_MENOR_ZERO);
            Id = id;
            DthrReg = DateTime.Now;
            ValidaContas(titulo, tipoConta, creditoAtivo, diaFechamento, diaVencimento, creditoLimite, creditoMaximo, status);
        }
        private void ValidaContas(string titulo, TiposContas tipoConta, bool creditoAtivo, int? diaFechamento, int? diaVencimento, bool creditoLimite, double? creditoMaximo, TiposStatus status)
        {
            ValidaTitulo(titulo);
            ValidaTipoDaConta(tipoConta);
            ValidaStatus(status);
            ValidaCreditoAtivo(creditoAtivo, diaFechamento, diaVencimento, creditoLimite, creditoMaximo);
        }
        private void ValidaCreditoAtivo(bool creditoAtivo, int? diaFechamento, int? diaVencimento, bool creditoLimite, double? creditoMaximo)
        {
            if (creditoAtivo)
            {
                ContasValidacao.Verifica(!diaVencimento.HasValue || !diaFechamento.HasValue, MensagensContas.FECHAMENTO_INVALIDO);
                ValidaFechamentoVencimento(diaFechamento!.Value, diaVencimento!.Value);

                if (creditoLimite)
                {
                    ValidaCreditoLimite(creditoMaximo);
                    CreditoLimite = creditoLimite;
                }
            }
            CreditoAtivo = creditoAtivo;
        }
        private void ValidaTitulo(string titulo)
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
            ContasValidacao.Verifica(!Enum.IsDefined(typeof(TiposStatus), status), MensagensBase.STATUS_INVALIDO);
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
        private void ValidaCreditoLimite(double? creditoMaximo)
        {
            ContasValidacao.Verifica(creditoMaximo == null, MensagensContas.ATUALIZA_CONTA_CREDITO_MAXIMO_NULO);
            ContasValidacao.Verifica(creditoMaximo <= 0, MensagensContas.CREDITO_MENOR_QUE_ZERO);
            CreditoMaximo = creditoMaximo;
        }
        public void AtualizaConta(ContasUsuarios usuarios, string? titulo, bool? creditoAtivo, TiposStatus? status, bool? creditoLimite, double? creditoMaximo, int? diaFechamento, int? diaVencimento)
        {
            ContasValidacao.Verifica(usuarios == null, MensagensBase.USUARIO_NAO_INFORMADO);
            ContasValidacao.Verifica((usuarios!.Acesso == TiposAcessos.Visualizador), MensagensContas.ATUALIZA_CONTA_USUARIO_SEM_PERMISSAO);
            ContasValidacao.Verifica(CreditoAtivo && creditoAtivo == false, MensagensContas.ATUALIZA_CONTA_CREDITO_ATIVO);

            if (titulo != null)
                ValidaTitulo(titulo);

            if (status is not null)
                ValidaStatus(status.Value);

            if (!CreditoAtivo && creditoAtivo == true)
            {
                CreditoAtivo = creditoAtivo.Value;
                ContasValidacao.Verifica((DiaFechamento is null && !diaFechamento.HasValue) || (DiaVencimento is null && !diaVencimento.HasValue), MensagensContas.FECHAMENTO_INVALIDO);
                ValidaFechamentoVencimento(diaFechamento.Value, diaVencimento.Value);
            }

            if (CreditoAtivo || creditoAtivo == true)
            {
                if (diaFechamento is not null && diaVencimento is not null)
                    ValidaFechamentoVencimento(diaFechamento.Value, diaVencimento.Value);


                if (creditoLimite is not null)
                {
                    CreditoLimite = creditoLimite.Value;
                    if (creditoLimite.Value)
                    {
                        if (CreditoMaximo is null)
                            ValidaCreditoLimite(creditoMaximo);
                    }
                    else
                    {
                        CreditoMaximo = null;
                    }
                }
            }
        }
    }
}
