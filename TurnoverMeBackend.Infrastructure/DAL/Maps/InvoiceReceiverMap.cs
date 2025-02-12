using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Infrastructure.DAL.Maps;

public class InvoiceReceiverMap : IEntityTypeConfiguration<InvoiceReceiver>
{
    public void Configure(EntityTypeBuilder<InvoiceReceiver> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.InvoiceId).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.OwnsOne(x => x.AddressValueObject, address =>
        {
            address.Property(x => x.City).HasColumnName("City").IsRequired();
            address.Property(x => x.Country).HasColumnName("Country").IsRequired();
            address.Property(x => x.PostCode).HasColumnName("PostCode").IsRequired();
            address.Property(x => x.Street).HasColumnName("Street").IsRequired();
            address.Property(x => x.StreetNumber).HasColumnName("StreetNumber").IsRequired();
            address.Property(x => x.FlatNumber).HasColumnName("FlatNumber").IsRequired();
        });
    }
}