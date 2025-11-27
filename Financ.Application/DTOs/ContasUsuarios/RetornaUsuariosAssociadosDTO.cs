using Financ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.DTOs.ContasUsuarios
{
    public record RetornaUsuariosAssociadosDTO(Guid IdUsuario,string Nome,string Email,TiposAcessos Permissao,TiposStatus Status);
}
