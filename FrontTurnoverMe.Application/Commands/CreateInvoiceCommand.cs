using FrontTurnoverMe.Application.Abstractions;
using FrontTurnoverMe.Application.DTO;

namespace FrontTurnoverMe.Application.Commands;

public record CreateInvoiceCommand(SaveInvoiceRequest InvoiceRequest) : ICommand;