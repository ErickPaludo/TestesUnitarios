using Financ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.DTOs.Contas
{
    public class FiltroContasDTO
    {
        public int? Id { get; set; }
        public string? Titulo { get; set; }
        public TiposStatus? Status { get; set; }
    }
}
