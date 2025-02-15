using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.Commands;
using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Application.Queries;
using TurnoverMeBackend.Domain.Entities;

namespace TurnoverMeBackend.Api.Endpoints;

public static class GroupEndpoints
{
    public static RouteGroupBuilder MapGroupEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("groups")
            .WithTags("groups");
            
        return group;
    }
    
    public record CreateGroupResponse(string Name, string Id);
}

