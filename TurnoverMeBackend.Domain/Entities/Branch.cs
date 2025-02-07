using Microsoft.AspNetCore.Identity;
using TurnoverMeBackend.Domain.Common;

namespace TurnoverMeBackend.Domain.Entities;

public class Branch : BaseEntity
{
    public string Name { get; set; }
    public IList<IdentityUser> Users { get; set; }
}