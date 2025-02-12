using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TurnoverMeBackend.Api.Endpoints;
using TurnoverMeBackend.Infrastructure.DAL;

namespace TurnoverMeBackend.Api.UglyServices;

public class RoleService(TurnoverMeDbContext dbContext,
    RoleManager<IdentityRole> roleManager) : BaseService(dbContext)
{
    public RoleDto[] GetRoles()
    {
        var roles = dbContext.Roles.ToListAsync().GetAwaiter().GetResult();
        return roles.Select(x => new RoleDto() { Id = x.Id, Name = x.Name }).ToArray();
    }

    public class RoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public void AddRole(CreateRole role)
    {
        if (!roleManager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
        {
            var result = roleManager.CreateAsync(new IdentityRole() { Name = role.Name }).GetAwaiter().GetResult();
            if (!result.Succeeded)
                throw new Exception("Creating role failed");
        }
    }
}