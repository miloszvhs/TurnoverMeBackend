using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.DTO;

namespace TurnoverMeBackend.Application.Commands;

public record CreateWorkflowCommand(CreateCircuitPathRequest request) : ICommand;
