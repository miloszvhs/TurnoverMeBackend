using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Application.Queries;
using Microsoft.Extensions.Logging;
using TurnoverMeBackend.Application.Helpers;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Infrastructure.DAL.Handlers;

public class GetInvoicesHandler(IInvoiceRepository invoiceRepository,
    ILogger<GetInvoicesHandler> logger)
    : IQueryHandler<GetInvoices, InvoiceDto[]>
{
    public InvoiceDto[] Handle(GetInvoices query)
    {
        InvoiceDto[] invoices;
        invoices = invoiceRepository
            .GetAll()
            .Select(InvoiceDtoHelper.MapToDto)
            .ToArray();
         
        return invoices;
    }

}