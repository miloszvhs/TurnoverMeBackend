using Microsoft.Extensions.Logging;
using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.Interfaces;

namespace TurnoverMeBackend.Application.Commands.Handlers;

public class AssignUserToGroupCommandHandler(IGroupRepository groupRepository,
    IUserRepository userRepository,
    ILogger<AssignUserToGroupCommandHandler> logger) : ICommandHandler<AssignUserToGroupCommand>
{
    public void Handle(AssignUserToGroupCommand command)
    {
        var group = groupRepository.Get(command.GroupId);
        if (group == null)
            throw new Exception($"Group '{command.GroupId}' doesnt exists");

        if (group.Users.FirstOrDefault(x => x.Id == command.UserId) != null)
            throw new Exception($"User '{command.UserId}' already is in group");

        var user = userRepository.Get(command.UserId);
        if (user == null)
            throw new Exception($"User '{command.UserId}' doesnt exists");
        
        if(!string.IsNullOrWhiteSpace(user.GroupId))
            logger.LogInformation($"User will be moved from group '{user.GroupId}' to '{command.GroupId}'");

        user.GroupId = command.GroupId;
    }
}