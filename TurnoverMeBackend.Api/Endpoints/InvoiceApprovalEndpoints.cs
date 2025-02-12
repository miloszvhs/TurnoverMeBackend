using TurnoverMeBackend.Api.UglyServices;

namespace TurnoverMeBackend.Api.Endpoints;

public static class InvoiceApprovalEndpoints
{
    public static RouteGroupBuilder MapInvoiceApprovalEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("invoices-approval")
            .WithTags("invoices-approval");

        group.MapGet("", (ApprovalService service, string invoiceId) => { return service.GetInvoiceApprovals(invoiceId); });
        
        group.MapPost("", (ApprovalService service) => {  });
        

        return group;
    }
}