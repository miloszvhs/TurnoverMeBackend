using Microsoft.EntityFrameworkCore;
using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Infrastructure.DAL;

namespace TurnoverMeBackend.Api.UglyServices;

public class ApprovalService(TurnoverMeDbContext dbContext) : BaseService(dbContext)
{
    public IEnumerable<string> Test()
    {
        foreach (var workflow in dbContext.Workflows)
            yield return workflow.Name;
    }

    public void SendInvoiceFurther()
    {

    }

    public void SendBackToChambers()
    {

    }

    public void SendInvoiceToLastUsedAcceptance()
    {

    }

    public ApprovalResponse[] GetInvoiceApprovals(string invoiceId)
    {
        var invoice = dbContext.Invoices.Include(x => x.Approvals).SingleOrDefaultAsync(x => x.Id == invoiceId).GetAwaiter().GetResult();
        if (invoice == null)
            throw new Exception("Invoice not found!");
        
        var workflow = invoice.Workflow;
        if (workflow == null)
            return [];
        
        var approvals = invoice.Approvals.Select(Map).ToArray();
        return approvals;
    }

    private ApprovalResponse Map(InvoiceApproval approval)
    {
        return new ApprovalResponse()
        {
            Executor = ,
            ExecutionTime = ,
            CreationTime = ,
            IsAccepted = ,
            StageName = approval.,
            Note = approval.Note
        };
    }
}

public class ApprovalResponse
{
    public string Executor { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ExecutionTime { get; set; }
    public string StageName { get; set; }
    public bool IsAccepted { get; set; }
    public string Note { get; set; }
}