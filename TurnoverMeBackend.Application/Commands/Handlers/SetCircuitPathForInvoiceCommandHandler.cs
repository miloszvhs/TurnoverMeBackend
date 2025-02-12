using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.Interfaces;

namespace TurnoverMeBackend.Application.Commands.Handlers;

public class SetCircuitPathForInvoiceCommandHandler(IInvoiceRepository invoiceRepository,
    ICircuitPathRepository circuitPathRepository) : ICommandHandler<SetWorkflowForInvoiceCommand>
{
    public void Handle(SetWorkflowForInvoiceCommand command)
    {
        var invoice = invoiceRepository.GetById(command.WorkflowForInvoiceRequest.InvoiceId);
        if (invoice == null)
            throw new Exception($"Invoice '{command.WorkflowForInvoiceRequest.InvoiceId}' doesnt exists");

        var circuitPath = circuitPathRepository.GetById(command.WorkflowForInvoiceRequest.CircuitPathId);
        if (circuitPath == null)
            throw new Exception($"Path '{command.WorkflowForInvoiceRequest.CircuitPathId}' doesnt exists");

        invoice.WorkflowId = circuitPath.Id;
    }
}