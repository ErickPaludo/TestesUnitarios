﻿using Financ.Application.Comun.Resultado;
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
    public sealed class AtualizarContaCommand : IRequest<Resultado<Contas>>
    {
        public int IdContaUsuario { get; private set; }
        public TiposStatus? Status { get; private set; }
        public string? Titulo { get; private set; }
        public int? DiaFechamento { get; private set; }
        public int? DiaVencimento { get; private set; }
        public double? CreditoLimite { get; private set; }

        public AtualizarContaCommand(int idContaUsuario, TiposStatus? status,string? titulo, int? diaFechamento, int? diaVencimento, double? creditoLimite)
        {
            IdContaUsuario = idContaUsuario;
            Status = status;
            Titulo = titulo;
            DiaFechamento = diaFechamento;
            DiaVencimento = diaVencimento;
            CreditoLimite = creditoLimite;
        }
    }
}
