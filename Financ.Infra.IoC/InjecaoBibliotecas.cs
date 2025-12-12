using Financ.Domain.Interfaces.Repositorios;
using Financ.Domain.Interfaces;
using Financ.Infra.Data.Contexto;
using Financ.Infra.Data.Repositorios;
using Financ.Infra.Data;
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
    public static class InjecaoBibliotecas
    {
        public static void ConfigurarInjecaoBibliotecas(this IServiceCollection services )
        {
            services.AddSimpleMediator();
            services.AddScoped<IMediator, Mediator>();
        }
    }
}
