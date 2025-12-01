using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.DTOs
{
    public sealed class CadastrarContasDTO : BaseContasDTO
    {
        public bool CreditoAtivo { get; set; }
    }
}
