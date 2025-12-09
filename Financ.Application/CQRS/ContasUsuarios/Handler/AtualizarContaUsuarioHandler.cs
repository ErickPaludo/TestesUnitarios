using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Commands;
using Financ.Application.DTOs.ContasUsuarios.Get;
using Financ.Application.Interfaces.Usuarios;
using Financ.Application.Mapeamento;
using Financ.Domain.Enums;
using Financ.Domain.Interfaces;
using Financ.Domain.Validacoes;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Handler
{
    public class AtualizarContaUsuarioHandler : IRequestHandler<AtualizarContaUsuarioCommand, Resultado<RetornaCadastroContasUsuariosDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AtualizarContaUsuarioHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Resultado<RetornaCadastroContasUsuariosDTO>> Handle(AtualizarContaUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contaUsuario = await _unitOfWork.contasUsuariosRepositorio.BuscarObjetoUnico(x => x.IdConta == request.idConta && x.IdUsuario == request.idUsuario);

                if (contaUsuario is not null)
                {
                    if ((await _unitOfWork.contasUsuariosRepositorio.BuscarPorCondicao(
                           x => x.IdConta == request.idConta &&
                           x.IdUsuario != request.idUsuario &&
                           x.Acesso == TiposAcessos.Administrador &&
                           x.Status == TiposStatus.Ativo)).Count() > 0)
                    {
                        contaUsuario.AtualizaContasUsuario(request.acesso, request.status);
                        _unitOfWork.contasUsuariosRepositorio.Atualiza(contaUsuario);
                        await _unitOfWork.Commit();
                        return Resultado<RetornaCadastroContasUsuariosDTO>.GeraSucesso(ContasUsuariosMapper.ParaDTO(contaUsuario));
                    }
                    return Resultado<RetornaCadastroContasUsuariosDTO>.GeraFalha(Falha.ErroOperacional("Não é possível atualizar a conta, pois a mesma ficará sem administradores ativos!"));
                }
                else
                {
                    return Resultado<RetornaCadastroContasUsuariosDTO>.GeraFalha(Falha.NaoEncontrado("Conta não encontrada!"));
                }
            }catch(ContasUsuariosValidacao ex)
            {
                return Resultado<RetornaCadastroContasUsuariosDTO>.GeraFalha(Falha.ErroOperacional(ex.Message));
            }


            throw new NotImplementedException();
        }
    }
}
