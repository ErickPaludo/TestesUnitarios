using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financ.Domain.Enums;

namespace Financ.Application.DTOs.Contas.Get
{
    public sealed class RetornaContasDTO 
    {
        public int IdConta { get; set; }
        public string Titulo { get; set; }
        public TiposStatus Status { get; set; }
        public bool CreditoAtivo { get; set; }
        public bool CreditoLimite { get; set; }
        public double? CreditoMaximo { get; set; }
        public int? DiaFechamento { get; init; }
        public int? DiaVencimento { get; set; }
    }
}
