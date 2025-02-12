using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.DTO;

namespace TurnoverMeBackend.Application.Queries;

public record GetGroups : IQuery<GroupsResponse.GroupDto[]>;
