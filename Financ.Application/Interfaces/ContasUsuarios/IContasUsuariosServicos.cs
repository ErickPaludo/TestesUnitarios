using Financ.Application.DTOs.ContasUsuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Interfaces.ContasUsuarios
{
    public interface IContasUsuariosServicos
    {
        Task IncluiUsuarioNaConta(InclusaoContaUsuarioDTO contaUsuarioDTO);
    }
}
