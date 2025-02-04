using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.DTO;

namespace TurnoverMeBackend.Application.Queries;

public record GetInvoices : IQuery<InvoiceDto[]>
{
}