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
        app.AddAdminEndpoints();
        
        var group = app.MapGroup("invoices")
            .WithTags("Invoices")
            .RequireAuthorization();

        group.MapPost("/commit",
            (ICommandHandler<CreateInvoiceCommand> createInvoiceCommandHandler, CreateInvoiceCommand command) =>
            {
                createInvoiceCommandHandler.Handle(command);
            });

        group.MapGet("/invoices/user", (ClaimsPrincipal user, IQueryHandler<GetInvoicesForUser, InvoiceDto[]> getInvoicesHandler) 
            => getInvoicesHandler.Handle(new GetInvoicesForUser(user.Identity.Name)));
        
        return app;
    }
}