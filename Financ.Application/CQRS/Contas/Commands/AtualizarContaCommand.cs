using Financ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Commands
{
    public class AtualizarContaCommand
    {
        public string? Titulo { get; private set; }
        public int? DiaFechamento { get; private set; }
        public int? DiaVencimento { get; private set; }
        public double? CreditoLimite { get; private set; }

        public AtualizarContaCommand(string? titulo, int? diaFechamento, int? diaVencimento, double? creditoLimite)
        {
            Titulo = titulo;
            DiaFechamento = diaFechamento;
            DiaVencimento = diaVencimento;
            CreditoLimite = creditoLimite;
        }
    }
}
