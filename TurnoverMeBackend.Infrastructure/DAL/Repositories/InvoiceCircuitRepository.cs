using Microsoft.EntityFrameworkCore;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Infrastructure.DAL.Repositories;

public class InvoiceCircuitRepository(TurnoverMeDbContext dbContext) : IInvoiceCircuitRepository
{
    private readonly DbSet<InvoiceApproval> _invoiceCircuits = dbContext.InvoiceApprovals;
    
    public void Save(InvoiceApproval invoiceApproval)
    {
        _invoiceCircuits.Add(invoiceApproval);
    }

    public InvoiceApproval Get(string id)
    {
        return _invoiceCircuits.SingleOrDefault(x => x.Id == id);
    }

    public List<InvoiceApproval> GetAll()
    {
        return _invoiceCircuits.ToList();
    }
}