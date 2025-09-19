using Financ.Domain.Interfaces;
using Financ.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Infra.Data.Repositorios
{
    public class BaseRepositorio<T> : IBaseRepositorio<T> where T : class
    {
        private readonly AppContextoData _contexto;
        public BaseRepositorio(AppContextoData contexto)
        {
            _contexto = contexto;
        }
        public async Task<T> Adicionar(T entity)
        {
            _contexto.Set<T>().Add(entity);
            await _contexto.SaveChangesAsync();
            return entity;
        }
    }
}
