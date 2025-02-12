using TurnoverMeBackend.Domain.Common;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Domain.Entities.MainFlow;

public class Workflow : BaseEntity
{
    public string Name { get; set; }
    public IList<Stage> Stages { get; set; } = [];
    public IList<Invoice> Invoices { get; set; } = [];
}