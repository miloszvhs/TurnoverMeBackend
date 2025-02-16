using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurnoverMeBackend.Domain.Entities;

namespace TurnoverMeBackend.Infrastructure.DAL.Maps;

public class InvoiceApprovalHistoryMap: IEntityTypeConfiguration<InvoiceApprovalHistory>
{
    public void Configure(EntityTypeBuilder<InvoiceApprovalHistory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Status).IsRequired()
            .HasConversion<string>();
    }
}