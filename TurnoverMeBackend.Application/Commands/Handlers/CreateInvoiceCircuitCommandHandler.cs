using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Application.Commands.Handlers;

public class CreateInvoiceCircuitCommandHandler(IInvoiceCircuitRepository invoiceCircuitRepository) : ICommandHandler<CreateInvoiceCircuitCommand>
{
    public void Handle(CreateInvoiceCircuitCommand command)
    {
        var newInvoiceCircuit = new InvoiceApproval()
        {
            UserId = command.UserId,
            Note = null,
            AcceptationTime = null,
            InvoiceId = command.InvoiceId,
            Status = ApprovalStatus.AwaitingApprove,
            GroupId = command.GroupId,
            StageLevel = command.StageLevel,
            ApproverName = null
        };
        
        invoiceCircuitRepository.Save(newInvoiceCircuit);
    }
}