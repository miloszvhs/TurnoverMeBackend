using FrontTurnoverMe.Domain.Entities;

namespace FrontTurnoverMe.Application.Interfaces;

public interface IInvoiceNumberRepository
{
    InvoiceNumber GetLastInvoiceNumber(string type);
    void SaveOrUpdate(InvoiceNumber invoiceNumber);
}