using Microsoft.EntityFrameworkCore;
using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Domain.Entities.MainFlow;
using TurnoverMeBackend.Infrastructure.DAL;

namespace TurnoverMeBackend.Api.ApiServices;

public class GroupService(TurnoverMeDbContext dbContext) : BaseService(dbContext)
{
    public GroupsResponse.GroupDto GetGroup(string groupId)
    {
        var group = dbContext.Groups.Include(x => x.Users)
            .SingleOrDefaultAsync(x => x.Id == groupId)
            .GetAwaiter().GetResult();

        if (group == null)
            throw new Exception("Group not found!");

        return MapToGroupDto(group);

    }
    
    private GroupsResponse.GroupDto MapToGroupDto(Group group)
    {
        return new GroupsResponse.GroupDto
        {
            Id = group.Id,
            Name = group.Name,
            Users = group.Users.Select(user => new GroupsResponse.GroupDto.UserDto
            {
                Id = user.Id,
                Name = user.UserName ?? ""
            }).ToList()
        };
    }
}