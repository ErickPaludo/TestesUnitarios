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
        Task<Resultado<RetornaContasDTO>> CriarConta(CadastrarContasDTO contaDTO);
        Task<Resultado<RetornaContasDTO>> RetornarContas(int idContas);
        Task<Resultado<RetornaContasDTO>> AlterarConta(int idContaUsuario, AtualizaContaDTO contaDTO);
    }
}
