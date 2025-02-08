using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Infrastructure.DAL.Maps;

public class InvoiceCircuitConfigurationMap: IEntityTypeConfiguration<InvoiceCircuit>
{
    public void Configure(EntityTypeBuilder<InvoiceCircuit> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.From).IsRequired();
        builder.Property(x => x.To).IsRequired();
        builder.Property(x => x.IssueDate).IsRequired();
        builder.Property(x => x.Stage).IsRequired();
        builder.Property(x => x.Note);
        builder.Property(x => x.Status).IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.InvoiceId).IsRequired();
        builder.Property(x => x.UserId);
    }
}