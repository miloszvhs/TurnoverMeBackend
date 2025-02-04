using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Application.Interfaces;

public interface IInvoiceRepository
{
    Task<List<Invoice>> GetAllAsync();
    List<Invoice> GetAll();
    List<Invoice> GetForUser(string userGuid);
    Task<Invoice> GetByIdAsync(string id);
    Invoice GetById(string id);
    
    int Add(Invoice[] invoices);
    int Add(Invoice invoices);
    Task<int> AddAsync(Invoice[] invoices);
    Task<int> AddAsync(Invoice invoice);
    Task<List<string>> GetAllIdsAsync();
}