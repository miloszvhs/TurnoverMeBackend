using TurnoverMeBackend.Domain.Common;

namespace TurnoverMeBackend.Domain.Entities.MainFlow;

public class Group : BaseEntity
{
    public string Name { get; set; }
    public IList<ApplicationUser> Users { get; set; }
    public IList<Stage> Stages { get; set; }
}