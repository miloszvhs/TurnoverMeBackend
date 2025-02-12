using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Application.Queries;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Infrastructure.DAL.Handlers;

public class GetCircuitPathHandler(ICircuitPathRepository circuitPathRepository) : IQueryHandler<GetWorkflow, Workflow>,
    IQueryHandler<GetWorkflows, Workflow[]>
{
    public Workflow Handle(GetWorkflow query)
    {
        return circuitPathRepository.GetById(query.CircuitPathId);
    }

    public Workflow[] Handle(GetWorkflows query)
    {
        return circuitPathRepository.GetAll();
    }
}