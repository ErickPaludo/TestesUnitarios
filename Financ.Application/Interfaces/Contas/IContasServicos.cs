using Financ.Application.Comun.Resultadoado;
using Financ.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Interfaces.Contas
{
    public interface IContasServicos
    {
        Task<Resultado<RetornaContasDTO>> CriarConta(CadastrarContasDTO contaDTO);
        Task<Resultado<RetornaContasDTO>> RetornarContas(int idContas);
    }
}
