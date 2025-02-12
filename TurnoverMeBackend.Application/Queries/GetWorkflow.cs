using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Application.Queries;

public record GetWorkflows : IQuery<Workflow[]>
{
}

public record GetWorkflow(string CircuitPathId) : IQuery<Workflow>
{
}