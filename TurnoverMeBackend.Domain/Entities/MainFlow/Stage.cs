using TurnoverMeBackend.Domain.Common;

namespace TurnoverMeBackend.Domain.Entities.MainFlow;

public class Stage : BaseEntity
{
    public string Name { get; set; }
    public int Order { get; set; }
    public string GroupId { get; set; }
    public Group Group { get; set; }
}