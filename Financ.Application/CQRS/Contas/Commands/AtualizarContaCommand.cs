using Financ.Application.Comun.Resultado;
using Financ.Application.DTOs.Contas.Get;
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
    public sealed class AtualizarContaCommand : IRequest<Resultado<RetornaContasDTO>>
    {
        public int IdConta { get; private set; }
        public string IdUsuario { get; set; }
        public TiposStatus? Status { get; private set; }
        public string? Titulo { get; private set; }
        public bool? CreditoAtivo { get; private set; }
        public bool? CreditoLimite { get; private set; }
        public int? DiaFechamento { get; private set; }
        public int? DiaVencimento { get; private set; }
        public double? CreditoMaximo { get; private set; }

        public AtualizarContaCommand(int idContaUsuario, string idUsuario, bool? creditoAtivo, bool? creditoLimite, TiposStatus? status, string? titulo, int? diaFechamento, int? diaVencimento, double? creditoMaximo)
        {
            IdConta = idContaUsuario;
            IdUsuario = idUsuario;
            CreditoAtivo = creditoAtivo;
            CreditoLimite = creditoLimite;
            Status = status;
            Titulo = titulo;
            DiaFechamento = diaFechamento;
            DiaVencimento = diaVencimento;
            CreditoMaximo = creditoMaximo;
        }
    }
}
