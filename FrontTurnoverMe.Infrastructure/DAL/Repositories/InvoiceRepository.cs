using FrontTurnoverMe.Application.Interfaces;
using FrontTurnoverMe.Domain.Entities;
using FrontTurnoverMe.Domain.Entities.Invoices;
using Microsoft.EntityFrameworkCore;

namespace FrontTurnoverMe.Infrastructure.DAL.Repositories;

public class InvoiceRepository(InvoicesDbContext context) : IInvoiceRepository
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
        return _invoices.ToList();
    }

    public List<Invoice> GetForUser(string userGuid)
    {
        return _invoices
            .Where(x => x.Id == userGuid)
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