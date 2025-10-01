﻿using Financ.Domain.Interfaces;
using Financ.Domain.Interfaces.Repositorios;
using Financ.Infra.Data.Contexto;
using Financ.Infra.Data.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppContextoData _contexto;
        private IContasRepositorio _contasRepositorio;
        private IContasUsuariosRepositorio _contasUsuariosRepositorio;
        public UnitOfWork(AppContextoData contexto)
        {
            _contexto = contexto;
        }
        public IContasRepositorio contasRepositorio { get { return _contasRepositorio = _contasRepositorio ?? new ContasRepositorio(_contexto); } }
        public IContasUsuariosRepositorio contasUsuariosRepositorio { get { return _contasUsuariosRepositorio = _contasUsuariosRepositorio ?? new ContasUsuariosRepositorio(_contexto); } }
        public async Task Commit()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
