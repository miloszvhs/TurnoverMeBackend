using TurnoverMeBackend.Domain.Common;

namespace TurnoverMeBackend.Domain.Entities;

//to jest obieg, który np. wysyłamy z kancelarii do etapu 1.
//można wysłać albo do grupy, albo do konkretnego usera.

public class InvoiceApproval : BaseEntity
{
    public string InvoiceId { get; set; }
    public int StageLevel { get; set; }
    public string? GroupId { get; set; }
    public string? UserId { get; set; }
    public string? ApproverName { get; set; }
    public DateTime? AcceptationTime { get; set; }
    public string? Note { get; set; }
    public CircuitStatus Status { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime DueDate { get; set; }

    public enum CircuitStatus
    {
        AwaitingApprove = 0,
        Approved = 1,
        Rejected = 2,
    }
}

public class InvoiceApprovalHistory : BaseEntity
{
    public string InvoiceId { get; set; }
    public string? Executor { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ExecutionTime { get; set; }
    public string StageName { get; set; }
    public bool IsAccepted { get; set; }
    public string? Note { get; set; }
}