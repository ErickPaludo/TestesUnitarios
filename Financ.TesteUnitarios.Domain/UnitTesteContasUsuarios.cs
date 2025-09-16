using Financ.Domain.Entidades;
using Financ.Domain.Enums;
using Financ.Domain.Validacoes;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.TesteUnitarios.Domain
{
    public class UnitTesteContasUsuarios
    {
        [Fact(DisplayName = "Valida se ID explícito menor ou igual a 0 gera divergência")]
        public void ContasUsuario_ComId_Invalido_GeraDivergencia()
        {
            Action action = () => new ContasUsuario(0, 1, 1, TiposAcessos.Visualizador, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.ID_IGUAL_MENOR_ZERO);

            action = () => new ContasUsuario(-3, 1, 1, TiposAcessos.Visualizador, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.ID_IGUAL_MENOR_ZERO);
        }

        [Fact(DisplayName = "Valida se IdConta <= 0 gera divergência")]
        public void IdConta_MenorOuIgualZero_GeraDivergencia()
        {
            Action action = () => new ContasUsuario(0, 1, TiposAcessos.Visualizador, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.IDCONTA_IGUAL_MENOR_ZERO);
        }

        [Fact(DisplayName = "Valida se IdUsuario <= 0 gera divergência")]
        public void IdUsuario_MenorOuIgualZero_GeraDivergencia()
        {
            Action action = () => new ContasUsuario(1, 0, TiposAcessos.Visualizador, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.IDUSUARIO_IGUAL_MENOR_ZERO);
        }

        [Fact(DisplayName = "Valida se TipoAcesso inválido gera divergência")]
        public void TipoAcesso_Invalido_GeraDivergencia()
        {
            var acessoInvalido = (TiposAcessos)999;
            Action action = () => new ContasUsuario(1, 1, acessoInvalido, TiposStatus.Ativo, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.ACESSO_INVALIDO);
        }

        [Fact(DisplayName = "Valida se Status inválido gera divergência")]
        public void Status_Invalido_GeraDivergencia()
        {
            var statusInvalido = (TiposStatus)999;
            Action action = () => new ContasUsuario(1, 1, TiposAcessos.Visualizador, statusInvalido, DateTime.Now);
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.STATUS_INVALIDO);
        }

        [Fact(DisplayName = "Valida se DataRegistro diferente de hoje gera divergência")]
        public void DataRegistro_Invalida_GeraDivergencia()
        {
            Action action = () => new ContasUsuario(1, 1, TiposAcessos.Visualizador, TiposStatus.Ativo, DateTime.Now.AddDays(-1));
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.DATA_REGISTRO_INVALIDA);

            action = () => new ContasUsuario(1, 1, TiposAcessos.Visualizador, TiposStatus.Ativo, DateTime.Now.AddDays(1));
            action.Should().Throw<ValidacaoDominio>().WithMessage(MensagensDominio.DATA_REGISTRO_INVALIDA);
        }

        [Fact(DisplayName = "Cadastra ContasUsuario com sucesso")]
        public void ContasUsuario_Valido_NaoGeraDivergencia()
        {
            Action action = () => new ContasUsuario(1, 1, TiposAcessos.Visualizador, TiposStatus.Ativo, DateTime.Now);
            action.Should().NotThrow<ValidacaoDominio>();
        }

        [Fact(DisplayName = "Cadastra ContasUsuario com ID explícito com sucesso")]
        public void ContasUsuario_ComId_Valido_NaoGeraDivergencia()
        {
            Action action = () => new ContasUsuario(1, 1, 1, TiposAcessos.Administrador, TiposStatus.Ativo, DateTime.Now);
            action.Should().NotThrow<ValidacaoDominio>();
        }
    }
}
