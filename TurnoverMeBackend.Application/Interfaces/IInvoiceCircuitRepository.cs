using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Application.Interfaces;

public interface IInvoiceCircuitRepository
{
    void Save(InvoiceApproval invoiceApproval);
    InvoiceApproval Get(string id);
    List<InvoiceApproval> GetAll();
}