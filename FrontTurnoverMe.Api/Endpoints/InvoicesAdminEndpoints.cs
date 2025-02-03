using System.Security.Claims;
using FrontTurnoverMe.Application.Abstractions;
using FrontTurnoverMe.Application.Commands;
using FrontTurnoverMe.Application.DTO;
using FrontTurnoverMe.Application.Queries;

namespace FrontTurnoverMe.Endpoints;

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