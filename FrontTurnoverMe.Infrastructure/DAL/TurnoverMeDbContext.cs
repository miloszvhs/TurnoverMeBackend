using FrontTurnoverMe.Domain.Entities;
using FrontTurnoverMe.Domain.Entities.Invoices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrontTurnoverMe.Infrastructure.DAL;

public class TurnoverMeDbContext : IdentityDbContext
{
    public TurnoverMeDbContext(DbContextOptions<TurnoverMeDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var roles = new List<IdentityRole>
        {
            new IdentityRole {Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN"},
            new IdentityRole {Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER"}
        };
        
        modelBuilder.Entity<IdentityRole>()
            .HasData(roles);
        base.OnModelCreating(modelBuilder);
    }
}