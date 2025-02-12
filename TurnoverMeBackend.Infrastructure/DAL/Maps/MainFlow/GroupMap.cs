using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Infrastructure.DAL.Maps.MainFlow;

public class GroupMap : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Users)
            .WithOne()
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}