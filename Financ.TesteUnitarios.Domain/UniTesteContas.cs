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
            Action action = () => new Contas(0, "Teste x", TiposContas.Corrente, false, null, null, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensBase.ID_IGUAL_MENOR_ZERO);

            action = () => new Contas(-1, "Teste x", TiposContas.Corrente, false, null, null, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensBase.ID_IGUAL_MENOR_ZERO);
        }

        [Fact(DisplayName = "Valida se título é nulo ou vazio")]
        public void Titulo_NuloOuVazio_GeraDivergencia()
        {
            Action action = () => new Contas(null, TiposContas.Corrente, false, null, null, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TITULO_OBRIGATORIO);

            action = () => new Contas("", TiposContas.Corrente, false, null, null, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TITULO_OBRIGATORIO);
        }

        [Fact(DisplayName = "Valida se título tem entre 5 e 100 caracteres")]
        public void Titulo_QuantidadeCaracteres_GeraDivergencia()
        {
            Action action = () => new Contas("af", TiposContas.Corrente, false, null, null, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TITULO_TAMANHO_INVALIDO);

            string longo = new string('x', 150);
            action = () => new Contas(longo, TiposContas.Corrente, false, null, null, null, TiposStatus.Ativo);

            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TITULO_TAMANHO_INVALIDO);
        }

        [Fact(DisplayName = "Tipo conta é um Enum válido")]
        public void TipoConta_Invalido_GeraDivergencia()
        {
            var tipoContaInvalido = (TiposContas)999;
            Action action = () => new Contas("Teste x", tipoContaInvalido, false, null, null, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TIPO_CONTA_INVALIDO);
        }

        [Fact(DisplayName = "Status é um Enum válido")]
        public void Status_Invalido_GeraDivergencia()
        {
            var statusInvalido = (TiposStatus)999;
            Action action = () => new Contas("Teste x", TiposContas.Poupanca, false, null, null, null, statusInvalido);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.STATUS_INVALIDO);
        }

        [Fact(DisplayName = "Fechamento inválido")]
        public void DiaFechamento_Invalido_GeraDivergencia()
        {
            Action action = () => new Contas("Teste", TiposContas.Poupanca, true, 0, 10, 100, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.FECHAMENTO_INVALIDO);

            action = () => new Contas("Teste", TiposContas.Poupanca, true, 20, 10, 100, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.FECHAMENTO_INVALIDO);
        }

        [Fact(DisplayName = "Vencimento invalido")]
        public void DiaVencimento_Invalido_GeraDivergencia()
        {
            Action action = () => new Contas("Teste", TiposContas.Poupanca, true, 5, 4, 200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.VENCIMENTO_MENOR_FECHAMENTO);

            action = () => new Contas("Teste", TiposContas.Poupanca, true, 10, 13, 200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.VENCIMENTO_MINIMO_7_DIAS);

            action = () => new Contas("Teste", TiposContas.Poupanca, true, 1, 16, 200, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.VENCIMENTO_MAXIMO_12_DIAS);
        }

        [Fact(DisplayName = "Crédito limite maior que zero")]
        public void CreditoLimite_Maior_Que_Zero()
        {
            Action action = () => new Contas("Teste", TiposContas.Poupanca, true, 5, 15, -1, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.CREDITO_MENOR_QUE_ZERO);

            // quando crédito inativo → limite pode ser nulo
            action = () => new Contas("Teste", TiposContas.Poupanca, false, null, null, null, TiposStatus.Ativo);
            action.Should().NotThrow();
        }

        [Fact(DisplayName = "Cadastra conta com sucesso")]
        public void Conta_Valida_NaoGeraDivergencia()
        {
            Action action = () => new Contas("Teste x", TiposContas.Poupanca, true, 5, 15, 200, TiposStatus.Ativo);
            action.Should().NotThrow();
        }

        [Fact(DisplayName = "Não atualizar conta com usuário diferente de admin")]
        public void Atualiza_Conta_Usuario_Nao_Admin()
        {
            var conta = new Contas("Teste x", TiposContas.Poupanca, true, 5, 15, 200, TiposStatus.Ativo);
            var contausuario = new ContasUsuarios(1, 1, Guid.NewGuid(), TiposAcessos.Visualizador, TiposStatus.Ativo);

            Action action = () => conta.AtualizaConta(contausuario, "Teste y", true, TiposStatus.Desativado, 300, 2, 10);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.ATUALIZA_CONTA_USUARIO_SEM_PERMISSAO);
        }
        [Fact(DisplayName = "Não permitir desativar crédito ativo em conta já ativa")]
        public void Atualiza_Conta_DesativarCreditoAtivo_GeraDivergencia()
        {
            var conta = new Contas("Conta Teste", TiposContas.Corrente, true, 5, 12, 500, TiposStatus.Ativo);

            var contausuario = new ContasUsuarios(1,1,Guid.NewGuid(),TiposAcessos.Administrador,TiposStatus.Ativo);

            Action action = () => conta.AtualizaConta(contausuario,"Nova Conta",false,TiposStatus.Ativo,500,5,10);

            action.Should()
                .Throw<ContasValidacao>()
                .WithMessage(MensagensContas.ATUALIZA_CONTA_CREDITO_ATIVO);
        }

        [Fact(DisplayName = "Atualiza conta corretamente")]
        public void Atualiza_Conta_NaoGeraDivergencia()
        {
            var conta = new Contas("Teste x", TiposContas.Corrente, true, 5, 15, 200, TiposStatus.Ativo);
            var contausuario = new ContasUsuarios(1, 1, Guid.NewGuid(), TiposAcessos.Administrador, TiposStatus.Ativo);

            Action action = () => conta.AtualizaConta(contausuario, null, true, TiposStatus.Desativado, 300, 2, 10);
            action.Should().NotThrow();

            action = () => conta.AtualizaConta(contausuario, "Teste x", true, null, 300, 2, 10);
            action.Should().NotThrow();

            action = () => conta.AtualizaConta(contausuario, "Teste x", true, TiposStatus.Desativado, null, 2, 10);
            action.Should().NotThrow();

            action = () => conta.AtualizaConta(contausuario, "Teste x", true, TiposStatus.Desativado, 300, null, 10);
            action.Should().NotThrow();

            action = () => conta.AtualizaConta(contausuario, "Teste x", true, TiposStatus.Desativado, 300, 2, null);
            action.Should().NotThrow();

            conta = new Contas("Teste x", TiposContas.Corrente, false, 5, 15, 200, TiposStatus.Ativo);

            action = () => conta.AtualizaConta(contausuario, "Teste x", false, TiposStatus.Desativado, 300, 2, null);
            action.Should().NotThrow();
        }
    }
}
