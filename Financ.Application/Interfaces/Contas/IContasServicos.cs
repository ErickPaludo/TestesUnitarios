using Financ.Application.Comun.Resultado;
using Financ.Application.DTOs;
using Financ.Application.DTOs.Contas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Interfaces.Contas
{
    public interface IContasServicos
    {
        Task<Resultado<RetornaContasDTO>> CriarConta(Guid idUsuario,CadastrarContasDTO contaDTO);
        Task<Resultado<List<RetornaContasDTO>>> RetornarContas(FiltroContasDTO? filtros, Guid IdUsuario);
        Task<Resultado<RetornaContasDTO>> AlterarConta(int idContaUsuario,Guid IdUsuario, AtualizaContaDTO contaDTO);
    }
}
