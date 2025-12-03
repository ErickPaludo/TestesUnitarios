using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Querys;
using Financ.Application.DTOs.ContasUsuarios.Get;
using Financ.Domain.Interfaces;
using Financ.Domain.Interfaces.Autenticação;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Handler
{
    public class RetornaUsuariosAssociadosHandler : IRequestHandler<RetornaUsuariosAssociadosQuery, Resultado<List<RetornaUsuariosAssociadosDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuariosIdentityServicos _usuarioServicos;

        public RetornaUsuariosAssociadosHandler(IUnitOfWork unitOfWork, IUsuariosIdentityServicos usuarioServicos)
        {
            _unitOfWork = unitOfWork;
            _usuarioServicos = usuarioServicos;
        }

        public async Task<Resultado<List<RetornaUsuariosAssociadosDTO>>> Handle(RetornaUsuariosAssociadosQuery request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.contasUsuariosRepositorio.BuscarObjetoUnico(x => x.IdConta == request.IdConta && x.IdUsuario == request.IdUsuario) != null)
            {

                var contaUsuarios = await _unitOfWork.contasUsuariosRepositorio.ObterContasDoUsuario(x => x.IdConta == request.IdConta && x.IdUsuario != request.IdUsuario);

                if (contaUsuarios.Count() > 0)
                {
                    List<RetornaUsuariosAssociadosDTO> listaUsuarios = new List<RetornaUsuariosAssociadosDTO>();
                    foreach (var conta in contaUsuarios)
                    {
                        var usuario = _usuarioServicos.ObtemUsuario(conta.IdUsuario).Result;
                        listaUsuarios.Add(new RetornaUsuariosAssociadosDTO(
                          conta.IdUsuario,
                          usuario.PrimeiroNome,
                          usuario.Email,
                          conta.Acesso,
                          conta.Status
                        ));
                    }
                    return Resultado<List<RetornaUsuariosAssociadosDTO>>.GeraSucesso(listaUsuarios);
                }
                return Resultado<List<RetornaUsuariosAssociadosDTO>>.GeraFalha(Falha.NaoEncontrado("Somente você está associado a está conta!"));
            }
            return Resultado<List<RetornaUsuariosAssociadosDTO>>.GeraFalha(Falha.NaoEncontrado("Conta não encontrada!"));
        }
    }
}
