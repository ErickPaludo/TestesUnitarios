using Financ.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Infra.Data.Contexto
{
    public class AppContextoData : DbContext
    {
        public AppContextoData(DbContextOptions<AppContextoData> options){}
        public DbSet<Contas> Contas;
        public DbSet<ContasUsuarios> ContasUsuarios;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContextoData).Assembly);
        }
    }
}
