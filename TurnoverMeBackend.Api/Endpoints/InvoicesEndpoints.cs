using System.Security.Claims;
using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.Commands;
using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace TurnoverMeBackend.Api.Endpoints;

public static class InvoicesEndpoints
{
    public static WebApplication MapInvoiceEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("invoices")
            .WithTags("invoices")
            .RequireAuthorization();

        group.MapPost("/commit",
            (ICommandHandler<CreateInvoiceCommand> createInvoiceCommandHandler, CreateInvoiceCommand command) =>
            {
                createInvoiceCommandHandler.Handle(command);
            });

        group.MapGet("/invoices/user", (ClaimsPrincipal user, IQueryHandler<GetInvoicesForUser, InvoiceDto[]> getInvoicesHandler) 
            => getInvoicesHandler.Handle(new GetInvoicesForUser(user.Identity.Name)));
        
        group.MapGet("/invoices", (IQueryHandler<GetInvoices, InvoiceDto[]> getInvoicesHandler)
            => getInvoicesHandler.Handle(new GetInvoices()));
        
        return app;
    }
}