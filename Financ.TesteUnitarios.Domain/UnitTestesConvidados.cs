using Financ.Domain.Entidades;
using Financ.Domain.Enums;
using Financ.Domain.Validacoes.Mensagens;
using Financ.Domain.Validacoes;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.TesteUnitarios.Domain
{
    public class UnitTestesConvidados
    {
        [Fact(DisplayName = "Gera convite sem divergência")]
        public void Id_NuloOuVazio_GeraDivergencia()
        {
            Contas conta = new Contas(1,"Teste x", TiposContas.Poupanca, true, 5, 15, true, 200, TiposStatus.Ativo);
            Usuario usuarioRemetente = new Usuario(Guid.NewGuid().ToString(), "Joao", "Silva", "teste@teste.com");
            Usuario usuarioDestinatario = new Usuario(Guid.NewGuid().ToString(), "Marcos", "Costa", "marcos@teste.com");
            ContasUsuarios contasUsuarioRemetente = new ContasUsuarios(1, conta, usuarioRemetente.IdUsuario, TiposAcessos.Mestre, TiposStatus.Ativo);

            Action action = () => new Convites(contasUsuarioRemetente,usuarioDestinatario.IdUsuario,conta,TiposAcessos.Administrador);
            action.Should().NotThrow();
        }
    }
}
