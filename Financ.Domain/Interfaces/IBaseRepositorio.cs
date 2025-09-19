using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Interfaces
{
    public interface IBaseRepositorio<T> where T : class
    {
        Task<T> Adicionar(T entity);
    }
}
