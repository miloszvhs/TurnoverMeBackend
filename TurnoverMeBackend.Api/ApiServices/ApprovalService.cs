using Microsoft.EntityFrameworkCore;
using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Domain.Entities.Invoices;
using TurnoverMeBackend.Infrastructure.DAL;

namespace TurnoverMeBackend.Api.ApiServices;

public class ApprovalService(TurnoverMeDbContext dbContext) : BaseService(dbContext)
{
    public void SendInvoiceFurther(SendInvoice sendInvoice)
    {
        var invoice = dbContext.Invoices
            .Include(x => x.Approvals)
            .Include(x => x.Workflow)
            .ThenInclude(w => w.Stages)
            .SingleOrDefault(x => x.Id == sendInvoice.InvoiceId);
        if (invoice == null)
            throw new Exception("Invoice not found!");

        if (invoice.Approvals == null || !invoice.Approvals.Any())
        {
            var firstApproval = new InvoiceApproval
            {
                InvoiceId = sendInvoice.InvoiceId,
                StageLevel = 1,
                Status = InvoiceApproval.CircuitStatus.AwaitingApprove,
                CreationTime = DateTime.UtcNow,
                DueDate = invoice.DueDate
            };

            if (!string.IsNullOrEmpty(sendInvoice.UserId))
            {
                firstApproval.UserId = sendInvoice.UserId;
            }
            else if (!string.IsNullOrEmpty(sendInvoice.GroupId))
            {
                firstApproval.GroupId = sendInvoice.GroupId;
            }
            else
            {
                throw new Exception("Either UserId or GroupId must be provided.");
            }
            
            var workflow = dbContext
                .Workflows
                .Include(x => x.Stages)
                .SingleOrDefaultAsync(x => x.Id == sendInvoice.WorkflowId).GetAwaiter().GetResult();
            if (workflow == null)
                throw new Exception("Workflow doesnt exists");
            
            invoice.Status = Invoice.InvoiceStatus.PendingApproval;
            invoice.Workflow = workflow;
            invoice.WorkflowId = workflow.Id;
            dbContext.InvoiceApprovals.Add(firstApproval);
            dbContext.InvoiceApprovalsHistories.Add(CreateNewFrom(firstApproval));
            dbContext.SaveChanges();
            return;
        }

        var currentApproval = invoice.Approvals
            .OrderByDescending(x => x.StageLevel)
            .FirstOrDefault(x => x.Status == InvoiceApproval.CircuitStatus.AwaitingApprove);

        if (currentApproval == null)
            throw new Exception("No pending approval found!");

        currentApproval.Status = InvoiceApproval.CircuitStatus.Approved;
        currentApproval.AcceptationTime = DateTime.UtcNow;

        var nextStageLevel = currentApproval.StageLevel + 1;

        if (nextStageLevel >= invoice.Workflow.Stages.Max(x => x.Level))
        {
            invoice.Status = Invoice.InvoiceStatus.Approved;
            dbContext.SaveChanges();
            return;
        }

        var nextApproval = new InvoiceApproval
        {
            InvoiceId = sendInvoice.InvoiceId,
            StageLevel = nextStageLevel,
            Status = InvoiceApproval.CircuitStatus.AwaitingApprove,
            CreationTime = DateTime.UtcNow,
            DueDate = invoice.DueDate
        };

        if (!string.IsNullOrEmpty(sendInvoice.UserId))
        {
            nextApproval.UserId = sendInvoice.UserId;
        }
        else if (!string.IsNullOrEmpty(sendInvoice.GroupId))
        {
            nextApproval.GroupId = sendInvoice.GroupId;
        }
        else
        {
            throw new Exception("Either UserId or GroupId must be provided.");
        }

        dbContext.InvoiceApprovals.Add(nextApproval);
        dbContext.SaveChanges();

        InvoiceApprovalHistory CreateNewFrom(InvoiceApproval invoiceApproval)
        {
            return new InvoiceApprovalHistory()
            {
                Executor = invoiceApproval.ApproverName,
                CreationTime = invoiceApproval.CreationTime,
                IsAccepted = invoiceApproval.Status == InvoiceApproval.CircuitStatus.Approved,
                ExecutionTime = null,
                StageName = invoice.Workflow?.Stages.FirstOrDefault(x => x.Level == invoiceApproval.StageLevel)?.Name,
                Note = invoiceApproval.Note,
                InvoiceId = invoiceApproval.InvoiceId
            };
        }
    }

    public void SendBackToChambers()
    {

    }

    public void SendInvoiceToLastUsedAcceptance()
    {

    }

    public ApprovalResponse[] GetInvoiceApprovalHistories(string invoiceId)
    {
        var invoice = dbContext.Invoices
            .Include(x => x.Workflow)
            .Include(x => x.ApprovalsHistories)
            .SingleOrDefaultAsync(x => x.Id == invoiceId).GetAwaiter().GetResult();
        if (invoice == null)
            throw new Exception("Invoice not found!");
        
        var workflow = invoice.Workflow;
        if (workflow == null)
            return [];
        
        var approvalsHistories = invoice.ApprovalsHistories.Select(Map).ToArray();
        return approvalsHistories;
    }

    public InvoiceApproval GetInvoiceApproval(string invoiceApprovalId)
    {
        var approval = dbContext.InvoiceApprovals
            .SingleOrDefault(x => x.Id == invoiceApprovalId);
        if (approval == null)
            throw new Exception("Approval not found!");

        return approval;
    }
    public void SaveApprovalHistory(string invoiceId)
    {
        var invoice = dbContext.Invoices
            .Include(x => x.Approvals)
            .SingleOrDefault(x => x.Id == invoiceId);
        if (invoice == null)
            throw new Exception("Invoice not found!");

        var approval = invoice.Approvals
            .OrderByDescending(x => x.StageLevel)
            .FirstOrDefault(x => x.Status == InvoiceApproval.CircuitStatus.AwaitingApprove);

        if (approval == null)
            throw new Exception("No pending approval found!");

        var approvalHistory = new InvoiceApprovalHistory
        {
            InvoiceId = invoice.Id,
            Executor = approval.ApproverName,
            CreationTime = DateTime.UtcNow,
            ExecutionTime = approval.AcceptationTime,
            StageName = $"Stage {approval.StageLevel}",
            IsAccepted = approval.Status == InvoiceApproval.CircuitStatus.Approved,
            Note = approval.Note
        };

        dbContext.InvoiceApprovalsHistories.Add(approvalHistory);
        dbContext.SaveChanges();
    }

    public void EditApproval(EditApproval editApproval)
    {
        var approval = dbContext.InvoiceApprovals
            .SingleOrDefault(x => x.Id == editApproval.ApprovalId);
        if (approval == null)
            throw new Exception("Approval not found!");

        if(!editApproval.IsAccepted)
        {
            approval.Note = editApproval.Note; // Example update
            approval.Status = InvoiceApproval.CircuitStatus.Rejected;
        }

        dbContext.InvoiceApprovals.Update(approval);
        dbContext.SaveChanges();
    }
    
    public IEnumerable<InvoiceApprovalDto> GetGroupApprovals(string groupId)
    {
        return dbContext.InvoiceApprovals
            .Where(x => x.GroupId == groupId)
            .Join(dbContext.Invoices,
                approval => approval.InvoiceId,
                invoice => invoice.Id,
                (approval, invoice) => new InvoiceApprovalDto
                {
                    InvoiceId = approval.InvoiceId,
                    DocumentNumber = invoice.InvoiceNumber,
                    StageLevel = approval.StageLevel,
                    GroupId = approval.GroupId,
                    UserId = approval.UserId,
                    ApproverName = approval.ApproverName,
                    AcceptationTime = approval.AcceptationTime,
                    Note = approval.Note,
                    Status = approval.Status.ToString(),
                    CreationTime = approval.CreationTime,
                    DueDate = approval.DueDate,
                    InvoiceFileAsBase64 = invoice.InvoiceFileAsBase64
                })
            .ToList();
    }

    public IEnumerable<InvoiceApprovalDto> GetMyApprovals(string userId)
    {
        return dbContext.InvoiceApprovals
            .Where(x => x.UserId == userId && x.Status == InvoiceApproval.CircuitStatus.AwaitingApprove)
            .Join(dbContext.Invoices,
                approval => approval.InvoiceId,
                invoice => invoice.Id,
                (approval, invoice) => new InvoiceApprovalDto
                {
                    InvoiceId = approval.InvoiceId,
                    DocumentNumber = invoice.InvoiceNumber,
                    StageLevel = approval.StageLevel,
                    GroupId = approval.GroupId,
                    UserId = approval.UserId,
                    ApproverName = approval.ApproverName,
                    AcceptationTime = approval.AcceptationTime,
                    Note = approval.Note,
                    Status = approval.Status.ToString(),
                    CreationTime = approval.CreationTime,
                    DueDate = approval.DueDate
                })
            .ToList();
    }

    public IEnumerable<InvoiceApprovalDto> GetAcceptedApprovals(string userId)
    {
        return dbContext.InvoiceApprovals
            .Where(x => x.UserId == userId && x.Status == InvoiceApproval.CircuitStatus.Approved)
            .Join(dbContext.Invoices,
                approval => approval.InvoiceId,
                invoice => invoice.Id,
                (approval, invoice) => new InvoiceApprovalDto
                {
                    InvoiceId = approval.InvoiceId,
                    DocumentNumber = invoice.InvoiceNumber,
                    StageLevel = approval.StageLevel,
                    GroupId = approval.GroupId,
                    UserId = approval.UserId,
                    ApproverName = approval.ApproverName,
                    AcceptationTime = approval.AcceptationTime,
                    Note = approval.Note,
                    Status = approval.Status.ToString(),
                    CreationTime = approval.CreationTime,
                    DueDate = approval.DueDate
                })
            .ToList();
    }

    public void ClaimApproval(string approvalId, string userId)
    {
        var approval = dbContext.InvoiceApprovals
            .SingleOrDefault(x => x.Id == approvalId);
        if (approval == null)
            throw new Exception("Approval not found!");

        approval.UserId = userId;
        dbContext.SaveChanges();
    }

    public void ApproveApproval(string approvalId)
    {
        var approval = dbContext.InvoiceApprovals
            .SingleOrDefault(x => x.Id == approvalId);
        if (approval == null)
            throw new Exception("Approval not found!");

        approval.Status = InvoiceApproval.CircuitStatus.Approved;
        approval.AcceptationTime = DateTime.UtcNow;
        dbContext.SaveChanges();
    }

    public void RejectApproval(string approvalId, string note, string option)
    {
        var approval = dbContext.InvoiceApprovals
            .SingleOrDefault(x => x.Id == approvalId);
        if (approval == null)
            throw new Exception("Approval not found!");

        approval.Status = InvoiceApproval.CircuitStatus.Rejected;
        approval.Note = note;
        dbContext.SaveChanges();

        if (option == "back")
        {
            // Logic to send back
        }
        else if (option == "chambers")
        {
            // Logic to send to chambers
        }
    }
    
    private ApprovalResponse Map(InvoiceApprovalHistory approval)
    {
        return new ApprovalResponse()
        {
            Executor = approval.Executor,
            ExecutionTime = approval.ExecutionTime,
            CreationTime = approval.CreationTime,
            IsAccepted = approval.IsAccepted,
            StageName = approval.StageName,
            Note = approval.Note
        };
    }

    private InvoiceApprovalDto MapInvoiceApprovalDto(InvoiceApproval approval, Invoice invoice)
    {
        return new InvoiceApprovalDto()
        {
            InvoiceId = approval.InvoiceId,
            DocumentNumber = invoice.InvoiceNumber,
            StageLevel = approval.StageLevel,
            GroupId = approval.GroupId,
            UserId = approval.UserId,
            ApproverName = approval.ApproverName,
            AcceptationTime = approval.AcceptationTime,
            Note = approval.Note,
            Status = approval.Status.ToString(),
            CreationTime = approval.CreationTime,
            DueDate = approval.DueDate
        };
    }
}

public class InvoiceApprovalDto
{
    public string InvoiceId { get; set; }
    public string DocumentNumber { get; set; }
    public int StageLevel { get; set; }
    public string? GroupId { get; set; }
    public string? UserId { get; set; }
    public string? ApproverName { get; set; }
    public DateTime? AcceptationTime { get; set; }
    public string? Note { get; set; }
    public string Status { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime DueDate { get; set; }
    public string InvoiceFileAsBase64 { get; set; }
}

public class SendInvoice
{
    public string InvoiceId { get; set; }
    public string? UserId { get; set; }
    public string? GroupId { get; set; }
    public string WorkflowId { get; set; }
}

public class EditApproval
{
    public string ApprovalId { get; set; }
    public bool IsAccepted { get; set; }
    public string Note { get; set; }
}

public class ApprovalResponse
{
    public string Executor { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ExecutionTime { get; set; }
    public string StageName { get; set; }
    public string Status { get; set; }
    public bool IsAccepted { get; set; }
    public string Note { get; set; }
}