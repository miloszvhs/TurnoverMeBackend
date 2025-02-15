using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TurnoverMeBackend.Api.ApiServices;
using TurnoverMeBackend.Domain.Entities;

namespace TurnoverMeBackend.Api.Endpoints;

public static class CustomIdentityEndpoints
{
    public static void AddCustomIdentityEndpoints(this WebApplication webApplication)
    {
        var routeGroup = webApplication
            .MapGroup("authentication")
            .WithTags("authentication");

        routeGroup.MapPost("/register",
                (AuthService authService, LoginUser loginUser) => authService.RegisterUser(loginUser))
            .WithOpenApi();

        routeGroup.MapPost("/login", (AuthService authService, LoginUser loginUser) => authService.Login(loginUser))
            .WithOpenApi();
        
        routeGroup.MapPost("/change-password", (AuthService authService, ChangePasswordRequest changePassword) 
                => authService.ChangePassword(changePassword))
            .WithOpenApi();

        routeGroup.MapPost("/refresh",
                (AuthService authService, RefreshTokenModel refreshTokenModel) =>
                    authService.RefreshToken(refreshTokenModel))
            .WithOpenApi();
    }
}
    
    