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
        
        //TODO
        //dać termin płatności (Due Date)
        //Kontrahent
        //Data wystawienia faktury
        //Wartość na fakturze
        
        
        //zastanowic sie i dorobic feature
        //do zastąpywania inynch uzytkowników
        //pod ich nieobceność
        
        //Funkcjonalnosc dodatkowa - odrzuć do etapu w dół LUB odrzuć całkowicie do kancelarii
        //z wiadomością czemu zostało to odrzucone
        
        //od do - wyjebać do - tylko kto zatwierdził
        
        //TODO maile - ogarnać powiadomienia mailowe, że do kogoś przyszła faktura
        
        //admin powinien miec mozliwosc resetu hasła użytkownikowi
        //dodania użytkownika
    }
}