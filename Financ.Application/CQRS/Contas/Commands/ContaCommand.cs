using Financ.Domain.Entidades;
using Financ.Domain.Enums;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Commands
{
    public class ContaCommand : IRequest<int>
    {
        public TiposStatus Status { get; protected set; }
        public DateTime DthrReg { get; protected set; }
        public string? Titulo { get; private set; }
        public TiposContas TipoConta { get; private set; }
        public int DiaFechamento { get; private set; }
        public int DiaVencimento { get; private set; }
        public double CreditoLimite { get; private set; }

        public ContaCommand(string? titulo, int diaFechamento, int diaVencimento, double creditoLimite)
        {
            Status = TiposStatus.Ativo;
            DthrReg = DateTime.Now;
            Titulo = titulo;
            TipoConta = TiposContas.Corrente;
            DiaFechamento = diaFechamento;
            DiaVencimento = diaVencimento;
            CreditoLimite = creditoLimite;
        }
    }
}
