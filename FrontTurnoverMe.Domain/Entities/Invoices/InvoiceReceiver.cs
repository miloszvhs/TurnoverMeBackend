using FrontTurnoverMe.Domain.Common;
using FrontTurnoverMe.Domain.Entities.ValueObjects;

namespace FrontTurnoverMe.Domain.Entities.Invoices;

public class InvoiceReceiver : BaseEntity
{
    public string InvoiceId { get; set; }
    public string Name { get; set; }
    public InvoiceAddressValueObject AddressValueObject { get; set; }
    public string? TaxNumber { get; set; }
}