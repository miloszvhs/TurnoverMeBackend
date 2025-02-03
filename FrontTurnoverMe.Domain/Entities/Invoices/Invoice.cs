using FrontTurnoverMe.Domain.Common;

namespace FrontTurnoverMe.Domain.Entities.Invoices;

public class Invoice : BaseEntity
{
    public string? InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public InvoiceSeller Seller { get; set; }
    public InvoiceBuyer Buyer { get; set; }
    public InvoiceReceiver? Receiver { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public IList<InvoicePositionItem> Items { get; set; }
    public decimal TotalNetAmount { get; set; }
    public decimal TotalTaxAmount { get; set; }
    public decimal TotalAmountDue { get; set; }
    public string Currency { get; set; }
}
