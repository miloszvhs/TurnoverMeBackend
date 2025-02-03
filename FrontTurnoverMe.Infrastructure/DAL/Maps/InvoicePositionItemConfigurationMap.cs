﻿using FrontTurnoverMe.Domain.Entities;
using FrontTurnoverMe.Domain.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontTurnoverMe.Infrastructure.DAL.Maps;

public class InvoicePositionItemConfigurationMap : IEntityTypeConfiguration<InvoicePositionItem>
{
    public void Configure(EntityTypeBuilder<InvoicePositionItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.InvoiceId).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Unit).IsRequired();
        builder.Property(x => x.Quantity).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.UnitNetPrice).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.Discount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.NetValue).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.TaxRate).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.TaxAmount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.GrossValue).IsRequired().HasColumnType("decimal(18,2)");
    }
}