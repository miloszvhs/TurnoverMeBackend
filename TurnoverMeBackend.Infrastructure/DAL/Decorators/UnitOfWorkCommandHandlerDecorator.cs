using TurnoverMeBackend.Application.Abstractions;

namespace TurnoverMeBackend.Infrastructure.DAL.Decorators;

public class UnitOfWorkCommandHandlerDecorator<TCommand>
    : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly IUnitOfWork _unitOfWork;
    
    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IUnitOfWork unitOfWork)
    {
        _commandHandler = commandHandler;
        _unitOfWork = unitOfWork;
    }
    public void Handle(TCommand command)
    {
        _unitOfWork.Execute(() => _commandHandler.Handle(command));
    }
}