using TurnoverMeBackend.Domain.Common;

namespace TurnoverMeBackend.Domain.Entities.Invoices;

public class InvoicePositionItem : BaseEntity
{
    public string InvoiceId { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitNetPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal NetValue { get; set; }
    public decimal TaxRate { get; set; } 
    public decimal TaxAmount { get; set; }
    public decimal GrossValue { get; set; }
}