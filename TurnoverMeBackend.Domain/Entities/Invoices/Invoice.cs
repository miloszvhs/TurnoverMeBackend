using System.ComponentModel.DataAnnotations.Schema;
using TurnoverMeBackend.Domain.Common;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Domain.Entities.Invoices;

public class Invoice : BaseEntity
{
    public string InvoiceNumber { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? DueDate { get; set; }
    public InvoiceSeller? Seller { get; set; }
    public InvoiceBuyer Buyer { get; set; }
    public InvoiceReceiver? Receiver { get; set; }
    public IList<InvoicePositionItem> Items { get; set; }
    public IList<InvoiceApproval> Approvals { get; set; }
    public decimal TotalNetAmount { get; set; }
    public decimal TotalTaxAmount { get; set; }
    public decimal TotalGrossAmount { get; set; }
    public string Currency { get; set; }
    public string? Remarks { get; set; }
    public InvoiceStatus Status { get; set; }
    public string? WorkflowId { get; set; }
    public Workflow? Workflow { get; set; }

    public enum InvoiceStatus
    {
        New,
        PendingApproval,
        Approved,
        Rejected,
    }
}
