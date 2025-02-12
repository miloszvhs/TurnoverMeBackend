using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Infrastructure.DAL.Maps.MainFlow;

public class WorkflowMap : IEntityTypeConfiguration<Workflow>
{
    public void Configure(EntityTypeBuilder<Workflow> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .HasIndex(x => x.Name)
            .IsUnique();

        builder
            .HasMany(x => x.Stages)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}