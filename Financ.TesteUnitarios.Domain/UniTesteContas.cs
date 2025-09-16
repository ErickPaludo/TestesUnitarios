using Financ.Domain.Entidades;
using Financ.Domain.Enums;
using Financ.Domain.Validacoes;
using FluentAssertions;

namespace Financ.TesteUnitarios.Domain
{
    public class UniTesteContas
    {
        [Fact(DisplayName = "Valida se título é nulo ou vazio")]
        public void Titulo_NuloOuVazio_GeraDivergencia()
        {
            Action action = () => new Contas(null, TipoConta.Corrente, 10, 19, Status.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage("O título obrigatório");

            action = () => new Contas("", TipoConta.Corrente, 10, 19, Status.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage("O título obrigatório");
        }

        [Fact(DisplayName = "Valida se título é maior que 5 e menor que 100")]
        public void Titulo_QuantidadeCaracteres_GeraDivergencia()
        {
            Action action = () => new Contas("asdf", TipoConta.Corrente, 10, 19, Status.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage("Titulo deve conter entre 5 a 100 caracteres.");

            action = () => new Contas("111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111", TipoConta.Corrente, 10, 19, Status.Ativo, DateTime.Now);

            action.Should().Throw<ValidacaoDominio>().WithMessage("Titulo deve conter entre 5 a 100 caracteres.");
        }

        [Fact(DisplayName = "Tipo conta é um Enum válido")]
        public void TipoConta_Invalido_GeraDivergencia()
        {
            var tipoContaInvalido = (TipoConta)999;
            Action action = () => new Contas("Teste x", tipoContaInvalido, 10, 19, Status.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage("Tipo de conta inválida.");
        }

        [Fact(DisplayName = "Status é um Enum válido")]
        public void Status_Invalido_GeraDivergencia()
        {
            var statusInvalido = (Status)10;
            Action action = () => new Contas("Teste x", TipoConta.Poupanca, 10, 19, statusInvalido, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage("Status inválido.");
        }

        [Fact(DisplayName = "Fechamento inválido")]
        public void DiaFechamento_Invalido_GeraDivergencia()
        {
            var diaFechamentoMenor = 0;
            var diaFechamentoMaior = 26;

            Action action = () => new Contas("Teste x", TipoConta.Poupanca, diaFechamentoMenor, 19, Status.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage("Dia de fechamento inválido, deve estar entre dia 1 e 25.");

            action = () => new Contas("Teste x", TipoConta.Poupanca, diaFechamentoMaior, 19, Status.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage("Dia de fechamento inválido, deve estar entre dia 1 e 25.");
        }
        [Fact(DisplayName = "Vencimento invalido")]
        public void DiaVencimento_Invalido_GeraDivergencia()
        {
            var diaFechamento = 18;
            var diaVencimento = 17;

            Action action = () => new Contas("Teste x", TipoConta.Poupanca, diaFechamento, diaVencimento, Status.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage("O vencimento da fatura deve ser maior do que a data de fechamento.");

            diaFechamento = 11;
            diaVencimento = 14;
            action = () => new Contas("Teste x", TipoConta.Poupanca, diaFechamento, diaVencimento, Status.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage("O vencimento da fatura deve ser maior do que a data de fechamento.");

            diaFechamento = 2;
            diaVencimento = 20;
            action = () => new Contas("Teste x", TipoConta.Poupanca, diaFechamento, diaVencimento, Status.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage("O vencimento deve ser de no máximo 12 dias após o fechamento.");
        }
    }
}