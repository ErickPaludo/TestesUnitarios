using Financ.Domain.Entidades;
using Financ.Domain.Interfaces;
using Financ.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Infra.Data.Repositorios
{
    public class ContasRepositorio : BaseRepositorio<Conta> , IContasRepositorio
    {
        public ContasRepositorio(AppContextoData contexto) : base(contexto)
        {
        }
    }
}
