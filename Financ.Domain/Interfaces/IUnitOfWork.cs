﻿using Financ.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IContasRepositorio contasRepositorio { get; }
        IContasUsuariosRepositorio contasUsuariosRepositorio { get; }
        Task Commit();
    }
}
