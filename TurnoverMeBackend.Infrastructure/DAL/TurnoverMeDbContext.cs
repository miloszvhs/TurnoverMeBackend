using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TurnoverMeBackend.Infrastructure.DAL;

public class TurnoverMeDbContext : IdentityDbContext
{
    public TurnoverMeDbContext(DbContextOptions<TurnoverMeDbContext> dbContextOptions)
        : base(dbContextOptions)
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

        var user = new IdentityUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "admin",
            Email = "admin@admin.com",
            EmailConfirmed = true,
            SecurityStamp = string.Empty,
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };
        
        var passwordHasher = new PasswordHasher<IdentityUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, "admin");
        
        modelBuilder.Entity<IdentityUser>()
            .HasData(user);
        
        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(new IdentityUserRole<string>
            {
                RoleId = roles.First(x => x.Name == "Admin").Id,
                UserId = user.Id
            });
        
        base.OnModelCreating(modelBuilder);
    }
}