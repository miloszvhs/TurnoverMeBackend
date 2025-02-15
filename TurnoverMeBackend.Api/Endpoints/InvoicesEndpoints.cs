using System.Security.Claims;
using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.Commands;
using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace TurnoverMeBackend.Api.Endpoints;

public static class InvoicesEndpoints
{
    public static RouteGroupBuilder MapInvoiceEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("invoices")
            .WithTags("invoices");

        group.MapGet("/", (IQueryHandler<GetInvoices, InvoiceDto[]> getInvoicesHandler)
            => getInvoicesHandler.Handle(new GetInvoices()));
        
        group.MapGet("/user", (ClaimsPrincipal user, IQueryHandler<GetInvoicesForUser, InvoiceDto[]> getInvoicesHandler) 
            => getInvoicesHandler.Handle(new GetInvoicesForUser(user.Identity.Name)));
        
        group.MapPost("/commit",
            (ICommandHandler<CreateInvoiceCommand> createInvoiceCommandHandler, CreateInvoiceCommand command) =>
            {
                createInvoiceCommandHandler.Handle(command);
            });
        
        return group;
    }
}