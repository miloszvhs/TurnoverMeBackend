using Microsoft.AspNetCore.Mvc;
using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.Commands;
using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Application.Queries;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Api.Endpoints;

public static class WorkflowEndpoints
{
    public static RouteGroupBuilder AddWorkflowEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("workflow")
            .WithTags("workflow");
        
        group.MapGet("", (IQueryHandler<GetWorkflows, Workflow[]> getWorkflows)
            => getWorkflows.Handle(new GetWorkflows()));
        
        group.MapGet("/{workflow}", (IQueryHandler<GetWorkflow, Workflow> getCircuitPath, [FromRoute] string workflow)
            => getCircuitPath.Handle(new GetWorkflow(workflow)));
        
        group.MapPost("",
            ([FromBody] CreateCircuitPathRequest request, 
                [FromServices]ICommandHandler<CreateWorkflowCommand> createCircuitPathCommandHandler) =>
            {
                createCircuitPathCommandHandler.Handle(new CreateWorkflowCommand(request));
            });
        
        group.MapPost("/{workflowId}/invoice/{invoiceId}/",
            (ICommandHandler<SetWorkflowForInvoiceCommand> setCircuitPathForInvoiceCommandHandler, 
                [FromRoute] string workflowId,
                [FromRoute] string invoiceId) =>
            {
                setCircuitPathForInvoiceCommandHandler.Handle(new SetWorkflowForInvoiceCommand(new WorkflowForInvoiceRequest(workflowId, invoiceId)));
            });
        
        return group;
    }
}