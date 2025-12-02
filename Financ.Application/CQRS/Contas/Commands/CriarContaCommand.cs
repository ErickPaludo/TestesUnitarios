using Financ.Application.Comun.Resultado;
using Financ.Application.CQRS.Base.Command;
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
    public sealed class CriarContaCommand : BaseCommand, IRequest<Resultado<Contas>>
    {
        public string? Titulo { get; private set; }
        public TiposContas TipoConta { get; private set; }
        public bool CreditoAtivo { get; private set; }
        public int? DiaFechamento { get; private set; }
        public int? DiaVencimento { get; private set; }
        public bool CreditoLimite { get; private set; }
        public double? CreditoMaximo { get; private set; }

        public CriarContaCommand(string? titulo, bool creditoAtivo, bool creditoLimite, int? diaFechamento, int? diaVencimento, double? creditoMaximo)
        {
            Status = TiposStatus.Ativo;
            CreditoAtivo = creditoAtivo;
            CreditoLimite = !creditoAtivo ? false : creditoLimite;
            Titulo = titulo;
            TipoConta = TiposContas.Corrente;
            DiaFechamento = diaFechamento;
            DiaVencimento = diaVencimento;
            CreditoMaximo = creditoMaximo;
        }
    }
}
