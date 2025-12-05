using Financ.Application.Comun.Resultado;
using Financ.Application.DTOs.ContasUsuarios.Get;
using Financ.Domain.Enums;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Application.CQRS.Querys
{
    public class RetornaUsuariosAssociadosQuery : IRequest<Resultado<List<RetornaUsuariosAssociadosDTO>>>
    {
        public Guid IdUsuarioSolicitante { get; set; }
        public int IdConta { get; set; }
        public Guid? IdUsuario { get; set; }
        public string? NomeUsuario { get; set; }
        public TiposAcessos? Acesso { get; set; }
        public TiposStatus? Status { get; set; }

        public RetornaUsuariosAssociadosQuery(Guid idUsuarioSolicitante, int idConta, Guid? idUsuario, string? nomeUsuario, TiposAcessos? acesso, TiposStatus? status)
        {
            IdUsuarioSolicitante = idUsuarioSolicitante;
            IdConta = idConta;
            IdUsuario = idUsuario;
            NomeUsuario = nomeUsuario;
            Acesso = acesso;
            Status = status;
        }
    }
}
