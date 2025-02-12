using TurnoverMeBackend.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Infrastructure.DAL.Repositories;

public class InvoiceRepository(TurnoverMeDbContext context) : IInvoiceRepository
{
    private readonly DbSet<Invoice> _invoices = context.Invoices;

    public List<Invoice> GetInvoices() => _invoices.ToList();

    public void Save(Invoice invoice)
    {
        _invoices.Add(invoice);
        context.SaveChanges();
    }

    public Task<List<Invoice>> GetAllAsync()
    {
        return _invoices.ToListAsync();
    }

    public List<Invoice> GetAll()
    {
        return _invoices
            .Include(x => x.Buyer)
            .Include(x => x.Seller)
            .Include(x => x.Items)
            .Include(x => x.Approvals)
            .Include(x => x.Receiver)
            .ToList();
    }

    public List<Invoice> GetForUser(string userGuid)
    {
        return _invoices
            .Where(x => x.Approvals.Any(x => x.UserId == userGuid))
            .ToList();
    }

    public Task<Invoice> GetByIdAsync(string id)
    {
        return _invoices
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public Invoice GetById(string id)
    {
        return _invoices
            .Where(x => x.Id == id)
            .FirstOrDefault();
    }

    public int Add(Invoice[] invoices)
    {
        _invoices.AddRange(invoices);
        return context.SaveChanges();
    }

    public int Add(Invoice invoices)
    {
        _invoices.Add(invoices);
        return context.SaveChanges();
    }

    public Task<int> AddAsync(Invoice[] invoice)
    {
        _invoices.AddRange(invoice);
        return context.SaveChangesAsync();
    }

    public Task<int> AddAsync(Invoice invoice)
    {
        _invoices.Add(invoice);
        return context.SaveChangesAsync();
    }

    public Task<List<string>> GetAllIdsAsync()
    {
        return _invoices
            .Select(x => x.InvoiceNumber)
            .ToListAsync();
    }
}