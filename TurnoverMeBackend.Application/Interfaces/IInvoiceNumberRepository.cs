using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Application.Interfaces;

public interface IInvoiceNumberRepository
{
    InvoiceNumber GetLastInvoiceNumber(string type);
    void SaveOrUpdate(InvoiceNumber invoiceNumber);
}