using System.Security.Claims;
using FrontTurnoverMe.Application.Abstractions;
using FrontTurnoverMe.Application.Commands;
using FrontTurnoverMe.Application.DTO;
using FrontTurnoverMe.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FrontTurnoverMe.Endpoints;

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