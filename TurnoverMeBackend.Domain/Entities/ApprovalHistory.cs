using TurnoverMeBackend.Domain.Common;

namespace TurnoverMeBackend.Domain.Entities;

public class ApprovalHistory : BaseEntity
{
    public string InvoiceId { get; set; }
    public int UserId { get; set; }
    public string UserFullName { get; set; }
    public string Comment { get; set; }
    public string Status { get; set; }
    public DateTime ActionDate { get; set; } = DateTime.Now;
}