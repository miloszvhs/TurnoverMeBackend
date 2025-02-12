using TurnoverMeBackend.Infrastructure.DAL;

namespace TurnoverMeBackend.Api.UglyServices;

public abstract class BaseService(TurnoverMeDbContext dbContext)
{
    protected TurnoverMeDbContext dbContext = dbContext;
}