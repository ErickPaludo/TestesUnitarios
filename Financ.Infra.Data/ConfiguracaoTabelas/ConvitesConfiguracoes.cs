using Financ.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financ.Infra.Data.Identity;

namespace Financ.Infra.Data.ConfiguracaoTabelas
{
    public class ConvitesConfiguracoes : IEntityTypeConfiguration<Convites>
    {
        public void Configure(EntityTypeBuilder<Convites> builder)
        {
            builder.ToTable("fnc_convites");
            builder.Property(e => e.IdConta).IsRequired();
            builder.Property(e => e.IdUsuarioRemetente).IsRequired();
            builder.Property(e => e.IdUsuarioDestinatario).IsRequired();
            builder.Property(e => e.Expiracao).IsRequired();
            builder.HasKey(e => e.Id);

            builder.HasOne<UsuarioIdentity>()
            .WithMany()
            .HasForeignKey(e => e.IdUsuarioRemetente)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Restrict);      
            
            builder.HasOne<UsuarioIdentity>()
            .WithMany()
            .HasForeignKey(e => e.IdUsuarioDestinatario)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Restrict); 
            
            builder.HasOne<Conta>()
            .WithMany()
            .HasForeignKey(e => e.IdConta)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
