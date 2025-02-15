using TurnoverMeBackend.Infrastructure.DAL;

namespace TurnoverMeBackend.Api.Middlewares;

public class TransactionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TransactionMiddleware> _logger;

    public TransactionMiddleware(RequestDelegate next, ILogger<TransactionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, TurnoverMeDbContext dbContext)
    {
        if (dbContext.Database.CurrentTransaction != null)
        {
            _logger.LogInformation("Istnieje już aktywna transakcja, kontynuuję...");
            await _next(context);
            return;
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            _logger.LogInformation("Transakcja rozpoczęta.");
            await _next(context);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            _logger.LogInformation("Transakcja zatwierdzona.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Błąd! Wycofuję transakcję.");
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            if (dbContext.Database.CurrentTransaction != null)
            {
                _logger.LogWarning("Transakcja nadal otwarta! Zakończam ją.");
                await dbContext.Database.CurrentTransaction.RollbackAsync();
            }
        }
    }
}