using Financ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.DTOs.Contas.Ptch
{
    public class AtualizaContaDTO 
    {   
        public string? Titulo { get; set; }
        public bool? CreditoAtivo { get; set; }
        public bool? CreditoLimite { get; set; }
        public double? CreditoMaximo { get; set; }
        public int? DiaFechamento { get; set; }
        public int? DiaVencimento { get; set; }
        public TiposStatus? Status { get; set; }
    }
}
