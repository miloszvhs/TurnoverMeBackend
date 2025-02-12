namespace TurnoverMeBackend.Application.Abstractions;

public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    void Handle(TCommand command);
}

public interface ICommandHandlerWithResult<in TCommand, out TResult> where TCommand : class, ICommandWithResult<TResult>
{
    TResult Handle(TCommand command);
}
