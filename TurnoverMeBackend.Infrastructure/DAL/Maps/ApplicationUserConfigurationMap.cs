using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurnoverMeBackend.Domain.Entities;

namespace TurnoverMeBackend.Infrastructure.DAL.Maps;

public class ApplicationUserConfigurationMap : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.InvoiceCircuits)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}