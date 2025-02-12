using TurnoverMeBackend.Application.Abstractions;

namespace TurnoverMeBackend.Application.Commands;

public record CreateGroupCommandWithResult(string Name) : ICommandWithResult<string>;
