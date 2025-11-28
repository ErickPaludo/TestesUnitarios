using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Commands;
using Financ.Domain.Entidades;
using Financ.Domain.Interfaces.Autenticação;
using Financ.Domain.Validacoes;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Handler
{
    public class CadastraUsuarioHandler : IRequestHandler<CadastraUsuarioCommand, Resultado<string>>
    {
        private readonly IAutenticacao _autenticacao;
        public CadastraUsuarioHandler(IAutenticacao autenticacao)
        {
            _autenticacao = autenticacao;
        }
        public async Task<Resultado<string>> Handle(CadastraUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Usuario usuario = new Usuario(request.PrimeiroNome, request.SegundoNome, request.Email);

                var usuarioCriado = await _autenticacao.RegistrarUsuario(usuario, request.Senha);

                return usuarioCriado.Item1 ? Resultado<string>.GeraSucesso("Usuário criado com sucesso!") : Resultado<string>.GeraFalha(Falha.ErroOperacional(usuarioCriado.Item2!));
                
            }
            catch (UsuariosValidacoes ex)
            { 
                return Resultado<string>.GeraFalha(Falha.ErroOperacional(ex.Message));
            }
            catch (Exception ex)
            {
                return Resultado<string>.GeraFalha(Falha.ErroOperacional());
            }
        }
    }
}
