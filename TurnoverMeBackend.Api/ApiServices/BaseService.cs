using TurnoverMeBackend.Infrastructure.DAL;

namespace TurnoverMeBackend.Api.ApiServices;

public abstract class BaseService(TurnoverMeDbContext dbContext)
{
    protected TurnoverMeDbContext dbContext = dbContext;
}