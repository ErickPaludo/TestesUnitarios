using Financ.Domain.Entidades;
using Financ.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Infra.Data.Contexto
{
    public class AppContextoData : IdentityDbContext<UsuarioIdentity>
    {
        public AppContextoData(DbContextOptions<AppContextoData> options) : base(options){}
        public DbSet<Contas> Contas;
        public DbSet<ContasUsuarios> ContasUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContextoData).Assembly);
        }
    }
}
