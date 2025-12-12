using Financ.Application.Comun.Resultado;
using Financ.Application.DTOs.Contas.Get;
using Financ.Application.DTOs.Contas.Get.Filtros;
using Financ.Application.DTOs.Contas.Ptch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Interfaces.Contas
{
    public interface IContasServicos
    {
        Task<Resultado<RetornaContasDTO>> CriarConta(string idUsuario,CadastrarContasDTO contaDTO);
        Task<Resultado<List<RetornaContasDTO>>> RetornarContas(FiltroContasDTO? filtros, string IdUsuario);
        Task<Resultado<RetornaContasDTO>> AlterarConta(int idContaUsuario, string IdUsuario, AtualizaContaDTO contaDTO);
    }
}
