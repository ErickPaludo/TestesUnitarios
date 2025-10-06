using Financ.Domain.Entidades;
using Financ.Domain.Enums;
using Financ.Domain.Validacoes;
using Financ.Domain.Validacoes.Mensagens;
using FluentAssertions;

namespace Financ.TesteUnitarios.Domain
{
    public class UniTesteContas
    {
        [Fact(DisplayName = "Valida id menor igual a 0")]
        public void Id_NuloOuVazio_GeraDivergencia()
        {
            Action action = () => new Contas(0,"Teste x", TiposContas.Corrente, 10, 19,200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensBase.ID_IGUAL_MENOR_ZERO);

            action = () => new Contas(-1,"Teste x", TiposContas.Corrente, 10, 19,200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensBase.ID_IGUAL_MENOR_ZERO);
        }
        [Fact(DisplayName = "Valida se título é nulo ou vazio")]
        public void Titulo_NuloOuVazio_GeraDivergencia()
        {
            Action action = () => new Contas(null, TiposContas.Corrente, 10, 19,200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TITULO_OBRIGATORIO);

            action = () => new Contas("", TiposContas.Corrente, 10, 19,200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TITULO_OBRIGATORIO);
        }

        [Fact(DisplayName = "Valida se título é maior que 3 e menor que 100")]
        public void Titulo_QuantidadeCaracteres_GeraDivergencia()
        {
            Action action = () => new Contas("af", TiposContas.Corrente, 10, 19,200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TITULO_TAMANHO_INVALIDO);

            action = () => new Contas("111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111", TiposContas.Corrente, 10, 19,200, TiposStatus.Ativo);

            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TITULO_TAMANHO_INVALIDO);
        }

        [Fact(DisplayName = "Tipo conta é um Enum válido")]
        public void TipoConta_Invalido_GeraDivergencia()
        {
            var tipoContaInvalido = (TiposContas)999;
            Action action = () => new Contas("Teste x", tipoContaInvalido, 10, 19,200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TIPO_CONTA_INVALIDO);
        }

        [Fact(DisplayName = "Status é um Enum válido")]
        public void Status_Invalido_GeraDivergencia()
        {
            var statusInvalido = (TiposStatus)999;
            Action action = () => new Contas("Teste x", TiposContas.Poupanca, 10, 19,200, statusInvalido);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.STATUS_INVALIDO);
        }

        [Fact(DisplayName = "Fechamento inválido")]
        public void DiaFechamento_Invalido_GeraDivergencia()
        {
            var diaFechamentoMenor = 0;
            var diaFechamentoMaior = 26;

            Action action = () => new Contas("Teste x", TiposContas.Poupanca, diaFechamentoMenor, 19,200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.FECHAMENTO_INVALIDO);

            action = () => new Contas("Teste x", TiposContas.Poupanca, diaFechamentoMaior, 19,200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.FECHAMENTO_INVALIDO);
        }
        [Fact(DisplayName = "Vencimento invalido")]
        public void DiaVencimento_Invalido_GeraDivergencia()
        {
            var diaFechamento = 0;
            var diaVencimento = 17;

            Action action = () => new Contas("Teste x", TiposContas.Poupanca, diaFechamento, diaVencimento,200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.FECHAMENTO_INVALIDO);

            diaFechamento = 12;
            diaVencimento = 11;
            
            action = () => new Contas("Teste x", TiposContas.Poupanca, diaFechamento, diaVencimento,200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.VENCIMENTO_MENOR_FECHAMENTO);

            diaFechamento = 11;
            diaVencimento = 14;
            action = () => new Contas("Teste x", TiposContas.Poupanca, diaFechamento, diaVencimento,200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.VENCIMENTO_MINIMO_7_DIAS);

            diaFechamento = 1;
            diaVencimento = 16;
            action = () => new Contas("Teste x", TiposContas.Poupanca, diaFechamento, diaVencimento,200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.VENCIMENTO_MAXIMO_12_DIAS);

            diaFechamento = 1;
            diaVencimento = 13;
            action = () => new Contas("Teste x", TiposContas.Poupanca, diaFechamento, diaVencimento, 200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.VENCIMENTO_MAXIMO_12_DIAS);
        }
     
        [Fact(DisplayName = "Crédito limite maior que zero")]
        public void CreditoLimite_Maior_Que_Zero()
        {
            Action action = () => new Contas("Teste x", TiposContas.Poupanca, 1, 8,-1, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.CREDITO_MENOR_QUE_ZERO);

            action = () => new Contas("Teste x", TiposContas.Poupanca, 1, 8, 0, TiposStatus.Ativo);
            action.Should().NotThrow<ContasValidacao>();
        }      
        [Fact(DisplayName = "Cadastra conta com sucesso")]
        public void Conta_Valida_NaoGeraDivergencia()
        {
            Action action = () => new Contas("Teste x", TiposContas.Poupanca, 1, 8,200, TiposStatus.Ativo);
            action.Should().NotThrow<ContasValidacao>();
        }
        [Fact(DisplayName = "Não atualizar conta com usuario diferente de admin")]
        public void Atualiza_Conta_Usuario_Nao_Admin()
        {
            var conta = new Contas("Teste x", TiposContas.Poupanca, 1, 8, 200, TiposStatus.Ativo);
            var contausaurio = new ContasUsuarios(1, 1, Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"), TiposAcessos.Visualizador, TiposStatus.Ativo);

            Action action = () => conta.AtualizaConta(contausaurio,"Teste y", TiposStatus.Desativado, 300, 2, 10);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.ATUALIZA_CONTA_USUARIO_SEM_PERMISSAO);
        }

    }
}