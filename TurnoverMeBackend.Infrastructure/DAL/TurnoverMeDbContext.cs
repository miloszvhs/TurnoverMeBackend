using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Domain.Entities.Invoices;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Infrastructure.DAL;

public class TurnoverMeDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceApproval> InvoiceApprovals { get; set; }
    public DbSet<InvoiceApprovalHistory> InvoiceApprovalsHistories { get; set; }
    public DbSet<InvoiceNumber> InvoiceNumbers { get; set; }
    public DbSet<Workflow> Workflows { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Stage> Stages { get; set; }

    public TurnoverMeDbContext(DbContextOptions<TurnoverMeDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var roles = new List<IdentityRole>
        {
            new IdentityRole {Id = "BFE154C0-CB46-4E46-B2B5-1419BE462FB4", Name = "Admin", NormalizedName = "ADMIN"},
            new IdentityRole {Id = "333154C0-CB46-4E46-B2B5-1419BE462FB4", Name = "User", NormalizedName = "USER"},
            new IdentityRole {Id = "66154C6-CB46-4E46-B2B5-1419BE462FB66", Name = "Chambers", NormalizedName = "CHAMBERS"}
        };
        
        modelBuilder.Entity<IdentityRole>()
            .HasData(roles);

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@admin.com",
            NormalizedEmail = "admin@admin.com".ToUpper(),
            EmailConfirmed = false
        };
        
        var passwordHasher = new PasswordHasher<ApplicationUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, "admin");
        
        modelBuilder.Entity<ApplicationUser>()
            .HasData(user);
        
        modelBuilder.Entity<Group>()
            .HasData(new List<Group>()
            {
                new Group()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "UsersGroup",
                    Users = []
                }
            });
        
        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(new IdentityUserRole<string>
            {
                RoleId = roles.First(x => x.Name == "Admin").Id,
                UserId = user.Id
            });
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}