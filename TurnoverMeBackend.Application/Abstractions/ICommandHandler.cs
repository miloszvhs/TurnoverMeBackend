namespace TurnoverMeBackend.Application.Abstractions;

public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    void Handle(TCommand command);
}
