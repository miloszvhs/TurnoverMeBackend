using FrontTurnoverMe.Application.Abstractions;
using FrontTurnoverMe.Application.DTO;

namespace FrontTurnoverMe.Application.Queries;

public record GetInvoicesForUser(string User) : IQuery<InvoiceDto[]>
{
}