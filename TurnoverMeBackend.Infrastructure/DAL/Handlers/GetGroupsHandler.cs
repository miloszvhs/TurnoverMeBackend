using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Application.Queries;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Infrastructure.DAL.Handlers;

public class GetGroupsHandler(IGroupRepository groupRepository) : IQueryHandler<GetGroups, GroupsResponse.GroupDto[]>
{
    public GroupsResponse.GroupDto[] Handle(GetGroups query)
    {
        var groups = groupRepository.GetAll();

        return groups.Select(MapToDto).ToArray();
    }

    private GroupsResponse.GroupDto MapToDto(Group group)
    {
        return new GroupsResponse.GroupDto()
        {
            Id = group.Id,
            Name = group.Name,
            Users = group.Users?.Select(x => new GroupsResponse.GroupDto.UserDto()
            {
                Id = x.Id,
                Name = x.UserName
            }).ToList()
        };
    }
}