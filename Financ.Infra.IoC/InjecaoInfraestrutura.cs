using Financ.Domain.Interfaces;
using Financ.Infra.Data.Contexto;
using Financ.Infra.Data.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddDbContext<AppContextoData>(op => op.UseSqlite(configure.GetConnectionString("SqlLite"), b => b.MigrationsAssembly(typeof(AppContextoData).Assembly.FullName))); //variavel b diz aonde gerar as migrations, pois o contexto esta em outro projeto
            services.AddScoped<IContasRepositorio, ContasRepositorio>();
            services.AddScoped<IContasUsuariosRepositorio, ContasUsuariosRepositorio>();
        }
    }
}
