using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Infrastructure.DAL.Maps;

public class InvoiceNumberMap : IEntityTypeConfiguration<InvoiceNumber>
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