using Financ.Domain.Entidades;
using Financ.Domain.Interfaces.Repositorios;
using Financ.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Infra.Data.Repositorios
{
    public class ConvitesRepositorio : BaseRepositorio<Convites>, IConvitesRepostorio
    {
        public ConvitesRepositorio(AppContextoData contexto) : base(contexto){}
    }
}
