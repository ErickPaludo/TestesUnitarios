using Financ.Application.DTOs.Contas.Get;
using Financ.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Financ.Application.Mapeamento
{
    public static class ContaMapper
    {
        public static RetornaContasDTO ParaDTO(Contas dto)
        {
            return new RetornaContasDTO
            {
                IdConta = dto.Id,
                Titulo = dto.Titulo,
                Status = dto.Status,
                CreditoAtivo = dto.CreditoAtivo,
                CreditoLimite = dto.CreditoLimite,
                DiaFechamento = dto.DiaFechamento,
                DiaVencimento = dto.DiaVencimento,
                CreditoMaximo = dto.CreditoMaximo
            };
        }
    }
}
