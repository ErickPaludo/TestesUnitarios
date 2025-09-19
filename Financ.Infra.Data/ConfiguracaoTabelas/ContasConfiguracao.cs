﻿using Financ.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Infra.Data.ConfiguracaoTabelas
{
    public class ContasConfiguracao : IEntityTypeConfiguration<Contas>
    {
        public void Configure(EntityTypeBuilder<Contas> builder)
        {
            builder.ToTable("fnc_contas");
            builder.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
            builder.Property(e => e.TipoConta).IsRequired();
            builder.Property(e => e.Status).IsRequired();
            builder.Property(e => e.DiaFechamento).HasMaxLength(16).IsRequired().HasPrecision(2,0);
            builder.Property(e => e.DiaVencimento).HasMaxLength(12).IsRequired().HasPrecision(2, 0);
            builder.Property(e => e.DthrReg).IsRequired();
            builder.HasKey(e => e.Id);
        }
    }
}
