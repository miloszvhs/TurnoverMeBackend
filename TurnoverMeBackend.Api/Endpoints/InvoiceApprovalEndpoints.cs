using TurnoverMeBackend.Api.ApiServices;

namespace TurnoverMeBackend.Api.Endpoints;

public static class InvoiceApprovalEndpoints
{
    public static RouteGroupBuilder MapInvoiceApprovalEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("invoices-approval")
            .WithTags("invoices-approval");
        
        group.MapGet("", (ApprovalService service, string invoiceApprovalId) => service.GetInvoiceApproval(invoiceApprovalId));
        
        group.MapPost("", (ApprovalService service, SendInvoice request) => {  service.SendInvoiceFurther(request); });
        
        group.MapGet("/history", (ApprovalService service, string invoiceId) => service.GetInvoiceApprovalHistories(invoiceId));
        
        group.MapPost("/history", (ApprovalService service, string invoiceId) => service.SaveApprovalHistory(invoiceId));

        group.MapPut("", (ApprovalService service, EditApproval editApproval) => {  service.EditApproval(editApproval); });
        
        group.MapGet("/group-approvals", (ApprovalService service, string groupId) => service.GetGroupApprovals(groupId));

        group.MapGet("/my-approvals", (ApprovalService service, string userId) => service.GetMyApprovals(userId));

        group.MapGet("/accepted-approvals", (ApprovalService service, string userId) => service.GetAcceptedApprovals(userId));

        group.MapPost("/claim", (ApprovalService service, string approvalId, string userId) => { service.ClaimApproval(approvalId, userId); });

        group.MapPost("/approve", (ApprovalService service, string approvalId) => { service.ApproveApproval(approvalId); });

        group.MapPost("/reject", (ApprovalService service, string approvalId, string note, string option) => { service.RejectApproval(approvalId, note, option); });
        
        return group;
    }
}