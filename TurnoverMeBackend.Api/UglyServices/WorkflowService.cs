using TurnoverMeBackend.Infrastructure.DAL;

namespace TurnoverMeBackend.Api.UglyServices;

public class WorkflowService(TurnoverMeDbContext dbContext) : BaseService(dbContext)
{
    public void AssignInvoiceToWorkflow()
    {
        
    }
}