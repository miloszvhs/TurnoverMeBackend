using TurnoverMeBackend.Application.Abstractions;

namespace TurnoverMeBackend.Application.Commands;

public record AssignUserToGroupCommand(string UserId, string GroupId) : ICommand;
