﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TurnoverMeBackend.Api.ApiServices;
using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.Commands;
using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Application.Queries;

namespace TurnoverMeBackend.Api.Endpoints;

public static class AdminEndpoints
{
    public static RouteGroupBuilder AddAdminEndpoints(this WebApplication app)
    {
        var adminGroup = app
            .MapGroup("admin")
            .WithTags("admin");

        adminGroup.MapGet("/roles", (RoleService service) => service.GetRoles());
        adminGroup.MapPost("/roles", (CreateRole role, RoleService service) => service.AddRole(role));

        adminGroup.MapGet("/users", (UserService service) => service.GetUsers());
        adminGroup.MapPost("/users", (
            CreateUser createUser,
            UserService service) =>
        {
            var createdUser = service.CreateUser(createUser);
            return Results.Created($"/users/{createdUser.Id}", createdUser);
        });

        adminGroup.MapPut("/users/{id}", (
            string id,
            UpdateUser updateUser,
            UserService service) =>
        {
            var updatedUser = service.UpdateUser(id, updateUser);
            return updatedUser is not null ? Results.Ok(updatedUser) : Results.NotFound();
        });

        adminGroup.MapDelete("/users/{id}", (
            string id,
            UserService service) =>
        {
            service.DeleteUser(id);
            return Results.NoContent();
        });
        
        adminGroup.MapGet("groups/", Ok<GroupsResponse>
            (IQueryHandler<GetGroups, GroupsResponse.GroupDto[]> getGroupsQueryHandler) =>
        {
            var result = getGroupsQueryHandler.Handle(new GetGroups());
            return TypedResults.Ok(new GroupsResponse() { Groups = result.ToList() });
        });
        adminGroup.MapGet("groups/group", (string groupId,
            GroupService service) => service.GetGroup(groupId));

        adminGroup.MapPost("groups/create", Ok<CreateGroupResponse>
        (ICommandHandlerWithResult<CreateGroupCommandWithResult, string> createGroupCommandHandler,
            CreateGroupCommandWithResult command) =>
        {
            var id = createGroupCommandHandler.Handle(command);
            var result = new CreateGroupResponse(command.Name, id);
            return TypedResults.Ok(result);
        });

        adminGroup.MapPost("groups/users/{userId}/group/{groupId}",
            (ICommandHandler<AssignUserToGroupCommand> assignUserToGroupCommandHandler,
                [FromRoute] string userId,
                [FromRoute] string groupId) =>
            {
                assignUserToGroupCommandHandler.Handle(new AssignUserToGroupCommand(userId, groupId));
            });

        return adminGroup;
    }
}


public record CreateGroupResponse(string Name, string Id);

public class CreateRole
{
    public string Name { get; set; }
}

public class CreateUser
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string GroupId { get; set; } = null!;
    public string RoleId { get; set; } = null!;
    public bool? ForcePasswordChange { get; set; }
}

public class UpdateUser
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? GroupId { get; set; }
    public string? RoleId { get; set; }
    public bool? ForcePasswordChange { get; set; }
}