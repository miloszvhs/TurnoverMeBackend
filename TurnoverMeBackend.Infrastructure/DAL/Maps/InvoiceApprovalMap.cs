using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Infrastructure.DAL.Maps;

public class InvoiceApprovalMap: IEntityTypeConfiguration<InvoiceApproval>
{
    public void Configure(EntityTypeBuilder<InvoiceApproval> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Note);
        builder.Property(x => x.Status).IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.InvoiceId).IsRequired();
        builder.Property(x => x.UserId);
        builder.Property(x => x.StageLevel);
        builder.Property(x => x.AcceptationTime);
        builder.Property(x => x.ApproverName);
    }
}