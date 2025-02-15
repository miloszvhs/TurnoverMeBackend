using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Application.Commands.Handlers;

public class CreateCircuitPathCommandHandler(ICircuitPathRepository circuitPathRepository,
    IGroupRepository groupRepository) : ICommandHandler<CreateWorkflowCommand>
{
    public void Handle(CreateWorkflowCommand command)
    {
        var request = command.request;
        
        var circuitPath = new Workflow
        {
            Name = request.name
        };

        foreach (var stage in request.stages)
        {
            var group = groupRepository.Get(stage.GroupId);
            if (group == null)
                throw new Exception($"Group '{stage.GroupId}' doesnt exists");
            
            circuitPath.Stages.Add(new Stage()
            {
                Name = stage.Name,
                Group = group,
                Level = stage.Order,
                GroupId = group.Id,
                GroupName = group.Name
            });
        }
        
        circuitPathRepository.Save(circuitPath);
    }
}