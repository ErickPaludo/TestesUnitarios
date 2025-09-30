using Financ.Domain.Entidades;
using Financ.Domain.Enums;
using Financ.Domain.Validacoes;
using FluentAssertions;

namespace Financ.TesteUnitarios.Domain
{
    public class UniTesteContas
    {
        [Fact(DisplayName = "Valida id menor igual a 0")]
        public void Id_NuloOuVazio_GeraDivergencia()
        {
            Action action = () => new Contas(0,"Teste x", TiposContas.Corrente, 10, 19,200, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.ID_IGUAL_MENOR_ZERO);

            action = () => new Contas(-1,"Teste x", TiposContas.Corrente, 10, 19,200, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.ID_IGUAL_MENOR_ZERO);
        }
        [Fact(DisplayName = "Valida se t�tulo � nulo ou vazio")]
        public void Titulo_NuloOuVazio_GeraDivergencia()
        {
            Action action = () => new Contas(null, TiposContas.Corrente, 10, 19,200, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.TITULO_OBRIGATORIO);

            action = () => new Contas("", TiposContas.Corrente, 10, 19,200, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.TITULO_OBRIGATORIO);
        }

        [Fact(DisplayName = "Valida se t�tulo � maior que 5 e menor que 100")]
        public void Titulo_QuantidadeCaracteres_GeraDivergencia()
        {
            Action action = () => new Contas("asdf", TiposContas.Corrente, 10, 19,200, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.TITULO_TAMANHO_INVALIDO);

            action = () => new Contas("111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111", TiposContas.Corrente, 10, 19,200, TiposStatus.Ativo, DateTime.Now);

            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.TITULO_TAMANHO_INVALIDO);
        }

        [Fact(DisplayName = "Tipo conta � um Enum v�lido")]
        public void TipoConta_Invalido_GeraDivergencia()
        {
            var tipoContaInvalido = (TiposContas)999;
            Action action = () => new Contas("Teste x", tipoContaInvalido, 10, 19,200, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.TIPO_CONTA_INVALIDO);
        }

        [Fact(DisplayName = "Status � um Enum v�lido")]
        public void Status_Invalido_GeraDivergencia()
        {
            var statusInvalido = (TiposStatus)999;
            Action action = () => new Contas("Teste x", TiposContas.Poupanca, 10, 19,200, statusInvalido, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.STATUS_INVALIDO);
        }

        [Fact(DisplayName = "Fechamento inv�lido")]
        public void DiaFechamento_Invalido_GeraDivergencia()
        {
            var diaFechamentoMenor = 0;
            var diaFechamentoMaior = 26;

            Action action = () => new Contas("Teste x", TiposContas.Poupanca, diaFechamentoMenor, 19,200, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.FECHAMENTO_INVALIDO);

            action = () => new Contas("Teste x", TiposContas.Poupanca, diaFechamentoMaior, 19,200, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.FECHAMENTO_INVALIDO);
        }
        [Fact(DisplayName = "Vencimento invalido")]
        public void DiaVencimento_Invalido_GeraDivergencia()
        {
            var diaFechamento = 0;
            var diaVencimento = 17;

            Action action = () => new Contas("Teste x", TiposContas.Poupanca, diaFechamento, diaVencimento,200, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.FECHAMENTO_INVALIDO);

            diaFechamento = 12;
            diaVencimento = 11;
            
            action = () => new Contas("Teste x", TiposContas.Poupanca, diaFechamento, diaVencimento,200, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.VENCIMENTO_MENOR_FECHAMENTO);

            diaFechamento = 11;
            diaVencimento = 14;
            action = () => new Contas("Teste x", TiposContas.Poupanca, diaFechamento, diaVencimento,200, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.VENCIMENTO_MINIMO_7_DIAS);

            diaFechamento = 1;
            diaVencimento = 16;
            action = () => new Contas("Teste x", TiposContas.Poupanca, diaFechamento, diaVencimento,200, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.VENCIMENTO_MAXIMO_12_DIAS);
        }
        [Fact(DisplayName = "Data resgitro inv�lida")]
        public void DataRegistro_Invalido_GeraDivergencia()
        {

            Action action = () => new Contas("Teste x", TiposContas.Poupanca, 1, 8,200, TiposStatus.Ativo, DateTime.Now.AddDays(-1));
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.DATA_REGISTRO_INVALIDA);
            
            action = () => new Contas("Teste x", TiposContas.Poupanca, 1, 8,200, TiposStatus.Ativo, DateTime.Now.AddDays(1));
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.DATA_REGISTRO_INVALIDA);
        } 
        [Fact(DisplayName = "Cr�dito limite maior que zero")]
        public void CreditoLimite_Maior_Que_Zero()
        {
            Action action = () => new Contas("Teste x", TiposContas.Poupanca, 1, 8,-1, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.CREDITO_MENOR_QUE_ZERO);

            action = () => new Contas("Teste x", TiposContas.Poupanca, 1, 8, 0, TiposStatus.Ativo, DateTime.Now);
            action.Should().NotThrow<ValidacaoDominio>();
        }      
        [Fact(DisplayName = "Cadastra conta com sucesso")]
        public void Conta_Valida_NaoGeraDivergencia()
        {
            Action action = () => new Contas("Teste x", TiposContas.Poupanca, 1, 8,200, TiposStatus.Ativo, DateTime.Now);
            action.Should().NotThrow<ValidacaoDominio>();
        }
    }
}