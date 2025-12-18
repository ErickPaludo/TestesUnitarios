using Financ.Domain.Entidades;
using Financ.Domain.Enums;
using Financ.Domain.Validacoes;
using Financ.Domain.Validacoes.Mensagens;
using FluentAssertions;

namespace Financ.TesteUnitarios.Domain
{
    public class UnitTesteContas
    {
        // ====================================================================
        // 1. TESTES DE CONSTRUTOR (CRIAÇÃO)
        // ====================================================================

        [Theory(DisplayName = "Construtor: IDs inválidos devem falhar")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Construtor_IdInvalido_GeraDivergencia(int id)
        {
            Action action = () => new Contas(id, "Teste", TiposContas.Corrente, false, null, null, false, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensBase.ID_IGUAL_MENOR_ZERO);
        }

        [Theory(DisplayName = "Construtor: Títulos inválidos (nulo, vazio, espaços) devem falhar")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Construtor_TituloNuloOuVazio_GeraDivergencia(string titulo)
        {
            Action action = () => new Contas(titulo, TiposContas.Corrente, false, null, null, false, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TITULO_OBRIGATORIO);
        }

        [Fact(DisplayName = "Construtor: Título com tamanho inválido (< 3 ou > 100) deve falhar")]
        public void Construtor_TituloTamanhoInvalido_GeraDivergencia()
        {
            // Menor que 3
            Action action = () => new Contas("AB", TiposContas.Corrente, false, null, null, false, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TITULO_TAMANHO_INVALIDO);

            // Maior que 100
            var tituloGigante = new string('A', 101);
            action = () => new Contas(tituloGigante, TiposContas.Corrente, false, null, null, false, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TITULO_TAMANHO_INVALIDO);
        }

        [Fact(DisplayName = "Construtor: Enums inválidos devem falhar")]
        public void Construtor_EnumsInvalidos_GeraDivergencia()
        {
            Action actionConta = () => new Contas("Teste", (TiposContas)99, false, null, null, false, null, TiposStatus.Ativo);
            actionConta.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TIPO_CONTA_INVALIDO);

            Action actionStatus = () => new Contas("Teste", TiposContas.Corrente, false, null, null, false, null, (TiposStatus)99);
            actionStatus.Should().Throw<ContasValidacao>().WithMessage(MensagensBase.STATUS_INVALIDO);
        }

        // ====================================================================
        // 2. TESTES DE DATAS (FECHAMENTO E VENCIMENTO)
        // ====================================================================

        [Theory(DisplayName = "Construtor: Dia Fechamento fora do range (1-16) deve falhar")]
        [InlineData(0)]
        [InlineData(17)]
        public void Construtor_DiaFechamentoInvalido_GeraDivergencia(int diaFechamento)
        {
            Action action = () => new Contas("Teste", TiposContas.Corrente, true, diaFechamento, 25, false, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.FECHAMENTO_INVALIDO);
        }

        [Fact(DisplayName = "Construtor: Vencimento menor ou igual ao Fechamento deve falhar")]
        public void Construtor_VencimentoMenorIgualFechamento_GeraDivergencia()
        {
            // Fechamento 5, Vencimento 5
            Action action = () => new Contas("Teste", TiposContas.Corrente, true, 5, 5, false, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.VENCIMENTO_MENOR_FECHAMENTO);

            // Fechamento 5, Vencimento 4
            action = () => new Contas("Teste", TiposContas.Corrente, true, 5, 4, false, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.VENCIMENTO_MENOR_FECHAMENTO);
        }

        [Fact(DisplayName = "Construtor: Diferença entre Fechamento e Vencimento deve respeitar limites (7 a 11 dias)")]
        public void Construtor_DiferencaDias_ValidacaoLimite()
        {
            // Diferença de 6 dias (Erro)
            Action action = () => new Contas("Teste", TiposContas.Corrente, true, 1, 7, false, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.VENCIMENTO_MINIMO_7_DIAS);

            // Diferença de 7 dias (Sucesso - Limite Inferior)
            action = () => new Contas("Teste", TiposContas.Corrente, true, 1, 8, false, null, TiposStatus.Ativo);
            action.Should().NotThrow();

            // Diferença de 11 dias (Sucesso - Limite Superior)
            action = () => new Contas("Teste", TiposContas.Corrente, true, 1, 12, false, null, TiposStatus.Ativo);
            action.Should().NotThrow();

            // Diferença de 12 dias (Erro)
            action = () => new Contas("Teste", TiposContas.Corrente, true, 1, 13, false, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.VENCIMENTO_MAXIMO_12_DIAS);
        }

        // ====================================================================
        // 3. TESTES DE CRÉDITO (ATIVO E LIMITE)
        // ====================================================================

        [Fact(DisplayName = "Construtor: Crédito Ativo exige datas informadas")]
        public void Construtor_CreditoAtivoSemDatas_GeraDivergencia()
        {
            Action action = () => new Contas("Teste", TiposContas.Corrente, true, null, null, false, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.FECHAMENTO_INVALIDO);
        }

        [Fact(DisplayName = "Construtor: Limite Ativo exige valor maior que zero")]
        public void Construtor_LimiteAtivoValorInvalido_GeraDivergencia()
        {
            // Valor Nulo
            Action action = () => new Contas("Teste", TiposContas.Corrente, true, 1, 10, true, null, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.ATUALIZA_CONTA_CREDITO_MAXIMO_NULO);

            // Valor Zero
            action = () => new Contas("Teste", TiposContas.Corrente, true, 1, 10, true, 0, TiposStatus.Ativo);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.CREDITO_MENOR_QUE_ZERO);
        }

        // ====================================================================
        // 4. TESTES DE ATUALIZAÇÃO (PERMISSÕES E TÍTULO)
        // ====================================================================

        [Fact(DisplayName = "AtualizaConta: Usuário Nulo ou Sem Permissão deve falhar")]
        public void Atualiza_UsuarioInvalido_GeraDivergencia()
        {
            var conta = new Contas("Teste", TiposContas.Corrente, false, null, null, false, null, TiposStatus.Ativo);

            // Usuario Nulo
            Action action = () => conta.AtualizaConta(null, "Novo", null, null, null, null, null, null);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensBase.USUARIO_NAO_INFORMADO);

            // Usuario Visualizador
            var usuarioVis = new ContasUsuarios(1, conta, "id", TiposAcessos.Visualizador, TiposStatus.Ativo);
            action = () => conta.AtualizaConta(usuarioVis, "Novo", null, null, null, null, null, null);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.ATUALIZA_CONTA_USUARIO_SEM_PERMISSAO);
        }

        [Theory(DisplayName = "AtualizaConta: Título vazio ou espaços deve falhar (se informado)")]
        [InlineData("")]
        [InlineData("   ")]
        public void Atualiza_TituloVazio_GeraDivergencia(string tituloInvalido)
        {
            var conta = new Contas("Teste", TiposContas.Corrente, false, null, null, false, null, TiposStatus.Ativo);
            var admin = new ContasUsuarios(1, conta, "id", TiposAcessos.Administrador, TiposStatus.Ativo);

            // Se passar null, ele ignora (comportamento correto). Se passar string vazia, deve validar.
            Action action = () => conta.AtualizaConta(admin, tituloInvalido, null, null, null, null, null, null);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.TITULO_OBRIGATORIO);
        }

        // ====================================================================
        // 5. TESTES DE ATUALIZAÇÃO (LÓGICA DE CRÉDITO E ESTADOS)
        // ====================================================================

        [Fact(DisplayName = "AtualizaConta: Não permite desativar Crédito Ativo")]
        public void Atualiza_DesativarCreditoAtivo_GeraDivergencia()
        {
            var conta = new Contas("Teste", TiposContas.Corrente, true, 1, 10, false, null, TiposStatus.Ativo);
            var admin = new ContasUsuarios(1, conta, "id", TiposAcessos.Administrador, TiposStatus.Ativo);

            Action action = () => conta.AtualizaConta(admin, null, false, null, null, null, null, null);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.ATUALIZA_CONTA_CREDITO_ATIVO);
        }

        [Fact(DisplayName = "AtualizaConta: Ativar crédito em conta existente requer datas")]
        public void Atualiza_AtivarCredito_SemDatas_GeraDivergencia()
        {
            // Conta nasceu SEM crédito
            var conta = new Contas("Teste", TiposContas.Corrente, false, null, null, false, null, TiposStatus.Ativo);
            var admin = new ContasUsuarios(1, conta, "id", TiposAcessos.Administrador, TiposStatus.Ativo);

            // Tenta ativar crédito (true) sem passar datas
            Action action = () => conta.AtualizaConta(admin, null, true, null, null, null, null, null);

            // O sistema deve barrar pois as datas são nulas na origem E no parametro
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.FECHAMENTO_INVALIDO);
        }

        [Fact(DisplayName = "AtualizaConta: Desativar Limite de Crédito deve limpar CreditoMaximo")]
        public void Atualiza_DesativarLimite_LimpaMaximo()
        {
            var conta = new Contas("Teste", TiposContas.Corrente, true, 1, 10, true, 500, TiposStatus.Ativo);
            var admin = new ContasUsuarios(1, conta, "id", TiposAcessos.Administrador, TiposStatus.Ativo);

            // Passa creditoLimite = false
            conta.AtualizaConta(admin, null, null, null, false, null, null, null);

            conta.CreditoLimite.Should().BeFalse();
            conta.CreditoMaximo.Should().BeNull();
        }

        [Fact(DisplayName = "AtualizaConta: Atualização parcial de Datas (Cruzamento de Dados)")]
        public void Atualiza_DatasParciais_ValidaConsistencia()
        {
            // Fechamento 1, Vencimento 10 (Dif 9)
            var conta = new Contas("Teste", TiposContas.Corrente, true, 1, 10, false, null, TiposStatus.Ativo);
            var admin = new ContasUsuarios(1, conta, "id", TiposAcessos.Administrador, TiposStatus.Ativo);

            // Cenário 1: Alterar Vencimento para 5 (usando Fechamento 1). Dif = 4 -> Falha
            Action action = () => conta.AtualizaConta(admin, null, null, null, null, null, null, 5);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.VENCIMENTO_MINIMO_7_DIAS);

            // Cenário 2: Alterar Fechamento para 5 (usando Vencimento 10). Dif = 5 -> Falha
            action = () => conta.AtualizaConta(admin, null, null, null, null, null, 5, null);
            action.Should().Throw<ContasValidacao>().WithMessage(MensagensContas.VENCIMENTO_MINIMO_7_DIAS);

            // Cenário 3: Sucesso - Fechamento 2 (Venc 10). Dif = 8 -> OK
            action = () => conta.AtualizaConta(admin, null, null, null, null, null, 2, null);
            action.Should().NotThrow();
            conta.DiaFechamento.Should().Be(2);
        }
    }
}
