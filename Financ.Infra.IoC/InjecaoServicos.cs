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

using Financ.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Financ.Domain.Interfaces.Autenticação;


namespace Financ.Infra.IoC
{
    public static class InjecaoServicos
    {
        public static void ConfigurarInjecaoServicos(this IServiceCollection services)
        {
            services.AddScoped<IUsuariosIdentityServicos, UsuariosIdentityServicos>();

            services.AddIdentity<UsuarioIdentity, IdentityRole>()
    .AddEntityFrameworkStores<AppContextoData>()
    .AddDefaultTokenProviders();
        }
    }
}
