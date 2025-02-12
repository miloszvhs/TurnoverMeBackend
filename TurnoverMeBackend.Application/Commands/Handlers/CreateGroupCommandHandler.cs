using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Application.Commands.Handlers;

public class CreateGroupCommandHandler(IGroupRepository groupRepository) : ICommandHandlerWithResult<CreateGroupCommandWithResult, string>
{
    public string Handle(CreateGroupCommandWithResult commandWithResult)
    {
        var newGroup = new Group()
        {
            Name = commandWithResult.Name
        };
        
        groupRepository.Save(newGroup);
        return newGroup.Id;
    }
}