using Financ.Application.DTOs.Convites.Get;
using Financ.Application.DTOs.Usuarios.Get;
using Financ.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.Mapeamento
{
    public static class ConvitesMapper
    {
        public static RetornoConvitesDTO ParaDTO(Convites convite) => new RetornoConvitesDTO(convite.Id, convite.Acesso, convite.Aceito);
    }
}
