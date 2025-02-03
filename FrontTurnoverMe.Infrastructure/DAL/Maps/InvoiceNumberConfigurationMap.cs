using FrontTurnoverMe.Domain.Entities;
using FrontTurnoverMe.Domain.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontTurnoverMe.Infrastructure.DAL.Maps;

public class InvoiceNumberConfigurationMap : IEntityTypeConfiguration<InvoiceNumber>
{
    public void Configure(EntityTypeBuilder<InvoiceNumber> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Number).IsRequired();
        builder.Property(x => x.LastUpdate).IsRequired();
        builder.Property(x => x.Refresh).IsRequired()
            .HasConversion<string>();
        
    }
}