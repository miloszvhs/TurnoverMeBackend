using Microsoft.Extensions.Logging;
using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Application.Helpers;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Application.Queries;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Infrastructure.DAL.Handlers;

public class GetInvoicesForUserHandler(IInvoiceRepository invoiceRepository,
    ILogger<GetInvoicesForUserHandler> logger)
    : IQueryHandler<GetInvoicesForUser, InvoiceDto[]>
{
    public InvoiceDto[] Handle(GetInvoicesForUser query)
    {
        InvoiceDto[] invoices;
        invoices = invoiceRepository
            .GetForUser(query.User)
            .Select(InvoiceDtoHelper.MapToDto)
            .ToArray();
         
        return invoices;
    }

}