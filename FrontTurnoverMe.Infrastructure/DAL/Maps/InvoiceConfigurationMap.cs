using FrontTurnoverMe.Domain.Entities;
using FrontTurnoverMe.Domain.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontTurnoverMe.Infrastructure.DAL.Maps;

public class InvoiceConfigurationMap : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.InvoiceNumber).IsRequired();
        builder.Property(x => x.IssueDate).IsRequired();
        builder.Property(x => x.DeliveryDate).IsRequired();
        builder.Property(x => x.TotalNetAmount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.TotalTaxAmount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.TotalAmountDue).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.Currency).IsRequired().HasMaxLength(3);

        builder.HasOne(x => x.Seller)
            .WithOne()
            .HasForeignKey<InvoiceSeller>(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Buyer)
            .WithOne()
            .HasForeignKey<InvoiceBuyer>(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Receiver)
            .WithOne()
            .HasForeignKey<InvoiceReceiver>(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}