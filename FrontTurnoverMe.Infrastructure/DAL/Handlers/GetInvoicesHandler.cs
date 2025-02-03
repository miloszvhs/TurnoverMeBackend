using FrontTurnoverMe.Application.Abstractions;
using FrontTurnoverMe.Application.DTO;
using FrontTurnoverMe.Application.Interfaces;
using FrontTurnoverMe.Application.Queries;
using FrontTurnoverMe.Domain.Entities.Invoices;
using Microsoft.Extensions.Logging;

namespace FrontTurnoverMe.Infrastructure.DAL.Handlers;

public class GetInvoicesHandler(IInvoiceRepository invoiceRepository,
    ILogger<GetInvoicesHandler> logger)
    : IQueryHandler<GetInvoices, InvoiceDto[]>
{
    public InvoiceDto[] Handle(GetInvoices query)
    {
        InvoiceDto[] invoices;
        invoices = invoiceRepository
            .GetAll()
            .Select(MapFromDto)
            .ToArray();
         
        return invoices;
    }

    private InvoiceDto MapFromDto(Invoice arg)
    {
        return new InvoiceDto
        {
            Number = arg.InvoiceNumber,
            Currency = arg.Currency,
        };
    }
}