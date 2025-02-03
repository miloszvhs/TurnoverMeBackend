using FrontTurnoverMe.Domain.Entities;
using FrontTurnoverMe.Domain.Entities.Invoices;
using Microsoft.EntityFrameworkCore;

namespace FrontTurnoverMe.Infrastructure.DAL;


public class InvoicesDbContext : DbContext
{
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceNumber> InvoiceNumbers { get; set; }
    
    public InvoicesDbContext(DbContextOptions<InvoicesDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}