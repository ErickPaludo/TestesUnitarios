using Financ.Domain.Interfaces.Autenticação;
using Financ.Infra.Data.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Infra.IoC
{
    public static class InjecaoAutenticacaoJWT
    {
        public static IServiceCollection ConfigurarInjecaoAutenticaoJWT(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddScoped<IAutenticacao, Autenticacao>();

            //informar o tipo de autenticacao JWT-Bearer
            // definir o modelo de desafio de autenticacao
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //habilita a autenticacao JWT usando o esquema e desafio definidos
           // validar o token
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
               //     valores validos
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!)),
                    ClockSkew = TimeSpan.Zero// se nao zerar,sera o tempo de vida do config mais 5 min
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<UsuarioIdentity>>();
                        var claimsPrincipal = context.Principal;

                        // Tenta pegar o usuário pelo ID do token
                        var usuario = await userManager.GetUserAsync(claimsPrincipal);

                        // 1. Verifica se o usuário ainda existe
                        if (usuario == null)
                        {
                            context.Fail("Usuário não encontrado.");
                            return;
                        }

                        // 2. (Opcional, mas recomendado) Verifica se o SecurityStamp mudou (ex: senha alterada)
                        // Isso requer que a claim do SecurityStamp esteja no token, ou você valida manualmente se necessário.
                        // Para Identity padrão, o token precisa ter sido gerado incluindo o SecurityStamp se quiser usar o validador nativo,
                        // ou você pode checar se o usuário está bloqueado:
                        if (await userManager.IsLockedOutAsync(usuario))
                        {
                            context.Fail("Usuário bloqueado.");
                            return;
                        }
                    }
                };
            });
            return services;
        }
    }
}
