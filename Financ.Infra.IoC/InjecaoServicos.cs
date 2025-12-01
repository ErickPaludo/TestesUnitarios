using Financ.Application.Interfaces.Contas;
using Financ.Application.Interfaces.ContasUsuarios;
using Financ.Application.Servicos.Contas;
using Financ.Application.Servicos;
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
using Financ.Application.Interfaces.Autenticação;
using Financ.Application.Servicos.Autenticaçao;
using Financ.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Financ.Domain.Interfaces.Autenticação;
using Financ.Application.Interfaces.Usuarios;
using Financ.Application.Servicos.Usuarios;

namespace Financ.Infra.IoC
{
    public static class InjecaoServicos
    {
        public static void ConfigurarInjecaoServicos(this IServiceCollection services)
        {
            services.AddScoped<IContasServicos, ContasServico>();
            services.AddScoped<IContasUsuariosServicos, ContasUsuariosServicos>();
            services.AddScoped<IAutenticacaoServicos, AutenticacaoServicos>();
            services.AddScoped<IUsuariosServicos, UsuariosServicos>();
            services.AddScoped<IUsuariosIdentityServicos, UsuariosIdentityServicos>();

            services.AddIdentity<UsuarioIdentity, IdentityRole>()
    .AddEntityFrameworkStores<AppContextoData>()
    .AddDefaultTokenProviders();
        }
    }
}
