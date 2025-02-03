namespace FrontTurnoverMe.Infrastructure.DAL;

public interface IUnitOfWork
{
    void Execute(Action action);
}

public class UnitOfWork : IUnitOfWork
{
    private readonly TurnoverMeDbContext _context;

    public UnitOfWork(TurnoverMeDbContext context)
    {
        _context = context;
    }

    public void Execute(Action action)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            action();
            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw;
        }
    }
}