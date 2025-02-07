using TurnoverMeBackend.Domain.Common;

namespace TurnoverMeBackend.Domain.Entities.Invoices;

public class InvoiceCircuit : BaseEntity
{
    public string InvoiceId { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public DateTime IssueDate { get; set; }
    public string Stage { get; set; }
    public string Note { get; set; }
}