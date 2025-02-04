using TurnoverMeBackend.Domain.Common;
using TurnoverMeBackend.Domain.Entities.ValueObjects;

namespace TurnoverMeBackend.Domain.Entities.Invoices;

public class InvoiceBuyer : BaseEntity
{
    public string InvoiceId { get; set; }
    public string Name { get; set; }
    public InvoiceAddressValueObject AddressValueObject { get; set; }
    public string TaxNumber { get; set; }
}