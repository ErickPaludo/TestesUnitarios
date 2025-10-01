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
        Task CriarConta(CadastrarContasDTO contaDTO);
    }
}
