using TurnoverMeBackend.Domain.Common;

namespace TurnoverMeBackend.Domain.Entities.MainFlow;

public class Stage : BaseEntity
{
    public string Name { get; set; }
    public int Level { get; set; }
    public string GroupId { get; set; }
    public string GroupName { get; set; }
    public Group Group { get; set; }
}