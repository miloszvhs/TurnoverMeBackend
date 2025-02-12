using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurnoverMeBackend.Domain.Entities.Invoices;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Infrastructure.DAL.Maps;

public class InvoiceMap : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.InvoiceNumber).IsRequired();
        builder.Property(x => x.IssueDate).IsRequired();
        builder.Property(x => x.DueDate).IsRequired();
        builder.Property(x => x.TotalNetAmount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.TotalTaxAmount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.TotalGrossAmount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.Currency).IsRequired();
        builder.Property(x => x.Remarks);
        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.WorkflowId).IsRequired(false);
        
        builder.HasOne(e => e.Workflow)
            .WithMany(o => o.Invoices)
            .HasForeignKey(e => e.WorkflowId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Seller)
            .WithOne()
            .HasForeignKey<InvoiceSeller>(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Buyer)
            .WithOne()
            .HasForeignKey<InvoiceBuyer>(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Receiver)
            .WithOne()
            .HasForeignKey<InvoiceReceiver>(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Approvals)
            .WithOne()
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}