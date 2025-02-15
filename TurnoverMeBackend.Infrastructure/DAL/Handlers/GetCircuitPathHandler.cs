using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Application.Queries;
using TurnoverMeBackend.Domain.Entities.Invoices;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Infrastructure.DAL.Handlers;

public record GetWorkflows : IQuery<WorkFlowResponseDto[]>
{
}

public record GetWorkflow(string CircuitPathId) : IQuery<WorkFlowResponseDto>
{
}

public class GetCircuitPathHandler(ICircuitPathRepository circuitPathRepository) : IQueryHandler<GetWorkflow, WorkFlowResponseDto>,
    IQueryHandler<GetWorkflows, WorkFlowResponseDto[]>
{
    public WorkFlowResponseDto Handle(GetWorkflow query)
    {
        var result = circuitPathRepository.GetById(query.CircuitPathId);
        return Map(result);
    }

    public WorkFlowResponseDto[] Handle(GetWorkflows query)
    {
        return circuitPathRepository.GetAll().Select(Map).ToArray();
    }

    private WorkFlowResponseDto Map(Workflow workflow)
    {
        return new WorkFlowResponseDto()
        {
            Id = workflow.Id,
            Name = workflow.Name,
            Invoices = workflow.Invoices,
            Stages = workflow.Stages?.Select(x => new WorkFlowResponseDto.WorkFlowResponseStageDto()
            {
                Name = x.Name,
                Level = x.Level,
                Group = x.Group,
                GroupId = x.GroupId,
                GroupName = x.GroupName,
                Order = x.Level
            }).ToList()
        };
    }
}

public class WorkFlowResponseDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IList<WorkFlowResponseStageDto> Stages { get; set; } = [];
    public IList<Invoice> Invoices { get; set; } = [];

    public class WorkFlowResponseStageDto
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
        public string GroupId { get; set; }
        public Group Group { get; set; }
        public string GroupName { get; set; }
    }
}