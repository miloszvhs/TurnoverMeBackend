using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.DTO;

namespace TurnoverMeBackend.Application.Queries;

public record GetInvoicesForUser(string User) : IQuery<InvoiceDto[]>
{
}