﻿using Financ.Application.Interfaces.Contas;
using Financ.Application.Interfaces.ContasUsuarios;
using Financ.Application.Servicos;
using Financ.Application.Servicos.Contas;
using Financ.Domain.Interfaces;
using Financ.Domain.Interfaces.Repositorios;
using Financ.Infra.Data;
using Financ.Infra.Data.Contexto;
using Financ.Infra.Data.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Infra.IoC
{
    public static class InjecaoInfraestrutura
    {
        public static void ConfigurarInjecaoInfraestrutura(this IServiceCollection services, IConfiguration configure)
        {

            services.AddDbContext<AppContextoData>(op => op.UseSqlite(configure.GetConnectionString("Sqlite"), b => b.MigrationsAssembly(typeof(AppContextoData).Assembly.FullName))); //variavel b diz aonde gerar as migrations, pois o contexto esta em outro projeto
            #region Repositorios
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IContasRepositorio, ContasRepositorio>();
            services.AddScoped<IContasUsuariosRepositorio, ContasUsuariosRepositorio>();
            #endregion
            #region Servicos Applicaiton
            services.AddScoped<IContasServicos, ContasServico>();
            services.AddScoped<IContasUsuariosServicos, ContasUsuariosServicos>();
            #endregion
            services.AddSimpleMediator();
            services.AddScoped<IMediator,Mediator>();
        }
    }
}
