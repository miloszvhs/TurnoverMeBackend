using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.DTO;

namespace TurnoverMeBackend.Application.Commands;

public record CreateWorkflowCommand(CreateWorkflowRequest request) : ICommand;
