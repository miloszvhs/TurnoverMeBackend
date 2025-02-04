using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Application.Queries;

namespace TurnoverMeBackend.Api.Endpoints;

public static class InvoicesAdminEndpoints
{
    public static WebApplication AddAdminEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("admin-invoices")
            .WithTags("AdminInvoices")
            .RequireAuthorization(x => x.RequireRole("Admin"));
        
        group.MapGet("/invoices", (IQueryHandler<GetInvoices, InvoiceDto[]> getInvoicesHandler)
                => getInvoicesHandler.Handle(new GetInvoices()));

        
        return app;
    }
}