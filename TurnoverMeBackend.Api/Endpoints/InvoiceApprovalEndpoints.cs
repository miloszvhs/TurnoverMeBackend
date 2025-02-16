using TurnoverMeBackend.Api.ApiServices;

namespace TurnoverMeBackend.Api.Endpoints;

public static class InvoiceApprovalEndpoints
{
    public static RouteGroupBuilder MapInvoiceApprovalEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("invoices-approval")
            .WithTags("invoices-approval");
        
        group.MapGet("", (ApprovalService service, string invoiceApprovalId) => service.GetInvoiceApproval(invoiceApprovalId));
        group.MapPost("", (ApprovalService service, SendInvoice request) => {  service.SendInvoiceToWorkflow(request); });
        group.MapPut("", (ApprovalService service, EditApproval editApproval) => {  service.EditApproval(editApproval); });
        
        group.MapGet("/history", (ApprovalService service, string invoiceId) => service.GetInvoiceApprovalHistories(invoiceId));
        group.MapPost("/history", (ApprovalService service, string invoiceId) => service.SaveApprovalHistory(invoiceId));

        group.MapGet("/group-approvals", (ApprovalService service, string groupId) => service.GetGroupApprovals(groupId));

        group.MapGet("/my-approvals", (ApprovalService service, string userId) => service.GetMyApprovals(userId));
        
        group.MapGet("/historical-approvals", (ApprovalService service, string userId) => service.GetHistoricalApprovals(userId));

        group.MapPost("/claim", (ApprovalService service, string approvalId, string userId) => { service.ClaimApproval(approvalId, userId); });
        group.MapPost("/approve", (ApprovalService service, ApproveApproval approveApproval) => { service.ApproveApproval(approveApproval); });
        group.MapPost("/reject", (ApprovalService service, RejectApproval rejectApproval) => { service.RejectApproval(rejectApproval); });
        
        return group;
    }
}