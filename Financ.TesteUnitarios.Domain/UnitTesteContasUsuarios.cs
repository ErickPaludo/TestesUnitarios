using Financ.Domain.Entidades;
using Financ.Domain.Enums;
using Financ.Domain.Validacoes;
using Financ.Domain.Validacoes.Mensagens;
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
            Action action = () => new ContasUsuarios(0, 1,  Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"), TiposAcessos.Visualizador, TiposStatus.Ativo);
            action.Should().Throw<ContasUsuariosValidacao>().WithMessage(MensagensBase.ID_IGUAL_MENOR_ZERO);

            action = () => new ContasUsuarios(-3, 1,  Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"), TiposAcessos.Visualizador, TiposStatus.Ativo);
            action.Should().Throw<ContasUsuariosValidacao>().WithMessage(MensagensBase.ID_IGUAL_MENOR_ZERO);
        }

        [Fact(DisplayName = "Valida se IdConta <= 0 gera divergência")]
        public void IdConta_MenorOuIgualZero_GeraDivergencia()
        {
            Action action = () => new ContasUsuarios(0,  Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"), TiposAcessos.Visualizador, TiposStatus.Ativo);
            action.Should().Throw<ContasUsuariosValidacao>().WithMessage(MensagensContasUsuarios.IDCONTA_IGUAL_MENOR_ZERO);
        }

        [Fact(DisplayName = "IdUsuario é vazio")]
        public void IdUsuario_MenorOuIgualZero_GeraDivergencia()
        {
            Action action = () => new ContasUsuarios(1,  Guid.Empty, TiposAcessos.Visualizador, TiposStatus.Ativo);
            action.Should().Throw<ContasUsuariosValidacao>().WithMessage(MensagensContasUsuarios.IDUSUARIO_VAZIO);
        }

        [Fact(DisplayName = "Valida se TipoAcesso inválido gera divergência")]
        public void TipoAcesso_Invalido_GeraDivergencia()
        {
            var acessoInvalido = (TiposAcessos)999;
            Action action = () => new ContasUsuarios(1,  Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"), acessoInvalido, TiposStatus.Ativo);
            action.Should().Throw<ContasUsuariosValidacao>().WithMessage(MensagensContasUsuarios.ACESSO_INVALIDO);
        }

        [Fact(DisplayName = "Valida se Status inválido gera divergência")]
        public void Status_Invalido_GeraDivergencia()
        {
            var statusInvalido = (TiposStatus)999;
            Action action = () => new ContasUsuarios(1, Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"), TiposAcessos.Visualizador, statusInvalido);
            action.Should().Throw<ContasUsuariosValidacao>().WithMessage(MensagensBase.STATUS_INVALIDO);
        }

        [Fact(DisplayName = "Cadastra ContasUsuario com sucesso")]
        public void ContasUsuario_Valido_NaoGeraDivergencia()
        {
            Action action = () => new ContasUsuarios(1,  Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"), TiposAcessos.Visualizador, TiposStatus.Ativo);
            action.Should().NotThrow<ContasUsuariosValidacao>();
        }

        [Fact(DisplayName = "Cadastra ContasUsuario com ID explícito com sucesso")]
        public void ContasUsuario_ComId_Valido_NaoGeraDivergencia()
        {
            Action action = () => new ContasUsuarios(1, 1, Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"), TiposAcessos.Administrador, TiposStatus.Ativo);
            action.Should().NotThrow<ContasUsuariosValidacao>();
        }

        [Fact(DisplayName = "Atualiza com acesso inválido")]
        public void Atualiza_Usuario_com_acesso_invalido()
        {
            var contaUsuario = new ContasUsuarios(1, 1, Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"), TiposAcessos.Administrador, TiposStatus.Ativo);

            Action action = () => contaUsuario.AtualizaContasUsuario((TiposAcessos)3, TiposStatus.Ativo);
            action.Should().Throw(MensagensContasUsuarios.ACESSO_INVALIDO);
        }

        [Fact(DisplayName = "Atualiza com status inválido")]
        public void Atualiza_Usuario_com_status_invalido()
        {
            var contaUsuario = new ContasUsuarios(1, 1, Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"), TiposAcessos.Administrador, TiposStatus.Ativo);

            Action action = () => contaUsuario.AtualizaContasUsuario(TiposAcessos.Visualizador, (TiposStatus)99);
            action.Should().Throw(MensagensBase.STATUS_INVALIDO);
        }

        [Fact(DisplayName = "Atualiza usuário com sucesso")]
        public void Atualiza_Usuario_sem_Divergencia()
        {
            var contaUsuario = new ContasUsuarios(1, 1, Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"), TiposAcessos.Administrador, TiposStatus.Ativo);

            Action action = () => contaUsuario.AtualizaContasUsuario(null,TiposStatus.Desativado);
            action.Should().NotThrow<ContasUsuariosValidacao>();

            action = () => contaUsuario.AtualizaContasUsuario(TiposAcessos.Administrador, null);
            action.Should().NotThrow<ContasUsuariosValidacao>();
        }
    }
}
