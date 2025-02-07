using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Application.Queries;

namespace TurnoverMeBackend.Api.Endpoints;

public static class InvoicesAdminEndpoints
{
    public static WebApplication AddAdminEndpoints(this WebApplication app)
    {
        var invoiceGroup = app
            .MapGroup("admin-invoices")
            .WithTags("AdminInvoices")
            .RequireAuthorization(x => x.RequireRole("Admin"));
        
        invoiceGroup.MapGet("/invoices", (IQueryHandler<GetInvoices, InvoiceDto[]> getInvoicesHandler)
                => getInvoicesHandler.Handle(new GetInvoices()));

        
        var chambersGroup = app
            .MapGroup("admin-chambers")
            .WithTags("admin-chambers")
            .RequireAuthorization(x => x.RequireRole("Admin"));

        chambersGroup.MapGet("/chambers", ()
            => {});
        
        chambersGroup.MapPost("/chambers", ()
            => {});
        
        chambersGroup.MapGet("/paths", ()
            => {});
        
        chambersGroup.MapPost("/paths/commit", ()
            => {});
        
        chambersGroup.MapPut("/paths/modify", ()
            => {});
        
        chambersGroup.MapDelete("/paths/delete", ()
            => {});
        
        return app;
    }
}