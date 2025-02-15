using TurnoverMeBackend.Infrastructure.DAL;

namespace TurnoverMeBackend.Api.ApiServices;

public class WorkflowService(TurnoverMeDbContext dbContext) : BaseService(dbContext)
{
    public void AssignInvoiceToWorkflow()
    {
        
    }
}