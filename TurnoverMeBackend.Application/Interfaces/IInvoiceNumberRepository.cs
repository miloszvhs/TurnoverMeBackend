using TurnoverMeBackend.Domain.Entities;

namespace TurnoverMeBackend.Application.Interfaces;

public interface IInvoiceNumberRepository
{
    InvoiceNumber GetLastInvoiceNumber(string type);
    void SaveOrUpdate(InvoiceNumber invoiceNumber);
}