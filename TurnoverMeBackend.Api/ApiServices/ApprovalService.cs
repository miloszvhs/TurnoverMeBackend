using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Domain.Entities.Invoices;
using TurnoverMeBackend.Infrastructure.DAL;

namespace TurnoverMeBackend.Api.ApiServices;

public class ApprovalService(TurnoverMeDbContext dbContext, UserManager<ApplicationUser> userManager) : BaseService(dbContext)
{
    public void SendInvoiceToWorkflow(SendInvoice sendInvoice)
    {
        var invoice = dbContext.Invoices
            .Include(x => x.Approvals)
            .Include(x => x.Workflow)
            .ThenInclude(w => w.Stages)
            .SingleOrDefault(x => x.Id == sendInvoice.InvoiceId);
        if (invoice == null)
            throw new Exception("Invoice not found!");

        var workflow = dbContext
            .Workflows
            .Include(x => x.Stages)
            .SingleOrDefaultAsync(x => x.Id == sendInvoice.WorkflowId).GetAwaiter().GetResult();
        if (workflow == null)
            throw new Exception("Workflow doesnt exists");
        
        if (invoice.Approvals == null || !invoice.Approvals.Any())
        {
            var firstApproval = new InvoiceApproval
            {
                InvoiceId = sendInvoice.InvoiceId,
                StageLevel = 1,
                Status = ApprovalStatus.AwaitingApprove,
                CreationTime = DateTime.Now,
                DueDate = invoice.DueDate,
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
            
            invoice.Status = Invoice.InvoiceStatus.PendingApproval;
            invoice.Workflow = workflow;
            invoice.WorkflowId = workflow.Id;
            dbContext.InvoiceApprovals.Add(firstApproval);
            dbContext.InvoiceApprovalsHistories.Add(CreateNewFrom(invoice, firstApproval));
            dbContext.SaveChanges();
            return;
        }
        
        var currentApproval = invoice.Approvals
            .OrderByDescending(x => x.StageLevel)
            .FirstOrDefault(x => x.Status == ApprovalStatus.AwaitingApprove);

        if (currentApproval == null)
            throw new Exception("No pending approval found!");

        currentApproval.Status = ApprovalStatus.Approved;
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
            Status = ApprovalStatus.AwaitingApprove,
            CreationTime = DateTime.Now,
            DueDate = invoice.DueDate,
            LastApprovalId = currentApproval.Id
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
            .FirstOrDefault(x => x.Status == ApprovalStatus.AwaitingApprove);

        if (approval == null)
            throw new Exception("No pending approval found!");

        var approvalHistory = new InvoiceApprovalHistory
        {
            InvoiceId = invoice.Id,
            Executor = approval.ApproverName,
            CreationTime = DateTime.UtcNow,
            ExecutionTime = approval.AcceptationTime,
            StageName = $"Stage {approval.StageLevel}",
            IsAccepted = approval.Status == ApprovalStatus.Approved,
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
            approval.Status = ApprovalStatus.Rejected;
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
                    Id = approval.Id,
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
                    InvoiceFileAsBase64 = invoice.InvoiceFileAsBase64,
                    LastApprovalId = approval.LastApprovalId,
                    ConstantlyRejected = approval.ConstantlyRejected
                })
            .ToList();
    }

    public IEnumerable<InvoiceApprovalDto> GetMyApprovals(string userId)
    {
        return dbContext.InvoiceApprovals
            .Where(x => x.UserId == userId 
                        && (x.Status == ApprovalStatus.AwaitingApprove
                        || (x.Status == ApprovalStatus.Rejected && x.ConstantlyRejected == false)))
            .Join(dbContext.Invoices,
                approval => approval.InvoiceId,
                invoice => invoice.Id,
                (approval, invoice) => new InvoiceApprovalDto
                {
                    Id = approval.Id,
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
                    InvoiceFileAsBase64 = invoice.InvoiceFileAsBase64,
                    LastApprovalId = approval.LastApprovalId,
                    ConstantlyRejected = approval.ConstantlyRejected
                })
            .ToList();
    }

    public IEnumerable<InvoiceApprovalDto> GetHistoricalApprovals(string userId)
    {
        return dbContext.InvoiceApprovals
            .Where(x => x.UserId == userId)
            .Where(x => (x.ConstantlyRejected && x.Status == ApprovalStatus.Rejected) || x.Status == ApprovalStatus.Approved)
            .Join(dbContext.Invoices,
                approval => approval.InvoiceId,
                invoice => invoice.Id,
                (approval, invoice) => new InvoiceApprovalDto
                {
                    Id = approval.Id,
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
                    InvoiceFileAsBase64 = invoice.InvoiceFileAsBase64,
                    LastApprovalId = approval.LastApprovalId,
                    ConstantlyRejected = approval.ConstantlyRejected
                })
            .ToList();
    }

    public void ClaimApproval(string approvalId, string userId)
    {
        var approval = dbContext.InvoiceApprovals
            .SingleOrDefault(x => x.Id == approvalId);
        if (approval == null)
            throw new Exception("Approval not found!");

        if (!string.IsNullOrWhiteSpace(approval.UserId))
            throw new Exception("Approval was already taken");
            
        approval.UserId = userId;
        dbContext.SaveChanges();
    }

    public void ApproveApproval(ApproveApproval approveApproval)
    {
        var approval = dbContext.InvoiceApprovals
            .SingleOrDefault(x => x.Id == approveApproval.ApprovalId);
        if (approval == null)
            throw new Exception("Approval not found!");

        var invoice = dbContext.Invoices.SingleOrDefault(x => x.Id == approval.InvoiceId);
        if (invoice == null)
            throw new Exception("Invoice not found!");

        var workflow = dbContext.Workflows.Include(x => x.Stages).SingleOrDefault(x => x.Id == invoice.WorkflowId);
        if (workflow == null)
            throw new Exception("Workflow not found!");
        
        var user = userManager.FindByIdAsync(approval.UserId).GetAwaiter().GetResult();
        if (user == null)
            throw new Exception("User not found!");
        
        approval.Status = ApprovalStatus.Approved;
        approval.AcceptationTime = DateTime.Now;
        approval.Note = approveApproval.Note;
        dbContext.InvoiceApprovalsHistories.Add(CreateAcceptedFrom(user, invoice, approval));
        
        SendFurther();
        dbContext.SaveChanges();

        void SendFurther()
        {
            var nextStageLevel = approval.StageLevel + 1;

            if (nextStageLevel >= workflow.Stages.Max(x => x.Level))
            {
                invoice.Status = Invoice.InvoiceStatus.Approved;
                dbContext.SaveChanges();
                return;
            }

            var nextApproval = new InvoiceApproval
            {
                InvoiceId = approval.InvoiceId,
                StageLevel = nextStageLevel,
                Status = ApprovalStatus.AwaitingApprove,
                CreationTime = DateTime.UtcNow,
                DueDate = invoice.DueDate,
                LastApprovalId = approval.Id,
                GroupId = workflow.Stages.SingleOrDefault(x => x.Level == nextStageLevel).GroupId
            };
            
            dbContext.InvoiceApprovals.Add(nextApproval);
        }
    }

    public void RejectApproval(RejectApproval rejectApproval)
    {
        var approval = dbContext.InvoiceApprovals
            .SingleOrDefault(x => x.Id == rejectApproval.ApprovalId);
        if (approval == null)
            throw new Exception("Approval not found!");
        
        var invoice = dbContext.Invoices
            .Include(x => x.Approvals)
            .Include(x => x.Workflow)
            .SingleOrDefault(x => x.Id == approval.InvoiceId);
        if (invoice == null)
            throw new Exception("Invoice not found");

        var user = dbContext.Users.SingleOrDefault(x => x.Id == approval.UserId);
        if (user == null)
            throw new Exception("User not found!");
        
        var workflow = dbContext.Workflows.Include(x => x.Stages).SingleOrDefault(x => x.Id == invoice.WorkflowId);
        if (user == null)
            throw new Exception("Workflow not found!");
        
        approval.Status = ApprovalStatus.Rejected;
        approval.Note = rejectApproval.Note;

        dbContext.InvoiceApprovalsHistories.Add(CreateRejectedFrom(user, invoice, approval));
        
        switch (rejectApproval.Option)
        {
            case "back":
                SendBack();
                break;
            case "chambers":
                SendToChambers();
                break;
            default:
                throw new NotSupportedException(rejectApproval.Option);
        }
        
        dbContext.SaveChanges();

        void SendBack()
        {
            if (string.IsNullOrWhiteSpace(approval.LastApprovalId))
            {
                invoice.Status = Invoice.InvoiceStatus.Rejected;
                return;
            }
            
            var lastApproval = dbContext.InvoiceApprovals.SingleOrDefault(x => x.Id == approval.LastApprovalId);
            lastApproval.Status = ApprovalStatus.Rejected;
            lastApproval.Note = GenerateRejectedNote();
        }

        void SendToChambers()
        {
            invoice.Status = Invoice.InvoiceStatus.Rejected;
            foreach (var invoiceApproval in invoice.Approvals)
            {
                invoiceApproval.Status = ApprovalStatus.Rejected;
                invoiceApproval.Note = GenerateUltimateRejectedNote();
                invoiceApproval.ConstantlyRejected = true;
            }
        }
        
        string GenerateRejectedNote()
        {
            return $"Odrzucone przez '{user.UserName}' " +
                   $"z '{workflow.Stages.SingleOrDefault(x => x.Level == approval.StageLevel).Name}'. " +
                   $"Powód: {approval.Note}";
        }

        string GenerateUltimateRejectedNote()
        {
            return $"Odrzucone i zwrócone do kancelarii przez '{user.UserName}' " +
                   $"z '{workflow.Stages.SingleOrDefault(x => x.Level == approval.StageLevel).Name}'. " +
                   $"Powód: {approval.Note}";
        }
    }

    InvoiceApprovalHistory CreateRejectedFrom(ApplicationUser user, Invoice invoice, InvoiceApproval invoiceApproval)
    {
        return new InvoiceApprovalHistory()
        {
            Executor = user.UserName,
            CreationTime = invoiceApproval.CreationTime,
            IsAccepted = false,
            ExecutionTime = DateTime.Now,
            StageName = invoice.Workflow?.Stages.FirstOrDefault(x => x.Level == invoiceApproval.StageLevel)?.Name,
            Note = invoiceApproval.Note,
            InvoiceId = invoiceApproval.InvoiceId,
            Status = ApprovalStatus.Rejected
        };
    }
    
    InvoiceApprovalHistory CreateAcceptedFrom(ApplicationUser user, Invoice invoice, InvoiceApproval invoiceApproval)
    {
        return new InvoiceApprovalHistory()
        {
            Executor = user.UserName,
            CreationTime = invoiceApproval.CreationTime,
            IsAccepted = true,
            ExecutionTime = invoiceApproval.AcceptationTime,
            StageName = invoice.Workflow?.Stages.FirstOrDefault(x => x.Level == invoiceApproval.StageLevel)?.Name,
            Note = invoiceApproval.Note,
            InvoiceId = invoiceApproval.InvoiceId,
            Status = ApprovalStatus.Approved
        };
    }
    
    InvoiceApprovalHistory CreateNewFrom(Invoice invoice, InvoiceApproval invoiceApproval)
    {
        return new InvoiceApprovalHistory()
        {
            Executor = "Kancelaria",
            CreationTime = invoiceApproval.CreationTime,
            IsAccepted = false,
            ExecutionTime = invoiceApproval.CreationTime,
            StageName = invoice.Workflow?.Stages.FirstOrDefault(x => x.Level == invoiceApproval.StageLevel)?.Name,
            Note = "Wysłanie faktury w obieg do zatwierdzenia",
            InvoiceId = invoiceApproval.InvoiceId,
            Status = ApprovalStatus.AwaitingApprove
        };
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
            Note = approval.Note,
            Status = approval.Status.ToString(),
        };
    }
}

public class RejectApproval
{
    public string ApprovalId { get; set; }
    public string Note { get; set; }
    public string Option { get; set; }
}

public class ApproveApproval
{
    public string ApprovalId { get; set; }
    public string Note { get; set; }
}

public class InvoiceApprovalDto
{
    public string Id { get; set; }
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
    public string? LastApprovalId { get; set; }
    public bool ConstantlyRejected { get; set; }
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