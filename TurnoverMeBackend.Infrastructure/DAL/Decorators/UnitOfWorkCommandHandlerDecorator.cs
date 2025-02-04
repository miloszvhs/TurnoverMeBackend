using TurnoverMeBackend.Application.Abstractions;

namespace TurnoverMeBackend.Infrastructure.DAL.Decorators;

public class UnitOfWorkCommandHandlerDecorator<TCommand>(
    ICommandHandler<TCommand> commandHandler,
    IUnitOfWork unitOfWork)
    : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    public void Handle(TCommand command)
    {
        unitOfWork.Execute(() => commandHandler.Handle(command));
    }
}