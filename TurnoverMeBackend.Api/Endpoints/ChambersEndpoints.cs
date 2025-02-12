namespace TurnoverMeBackend.Api.Endpoints;

public static class ChambersEndpoints
{
    public static RouteGroupBuilder AddChambersEndpoints(this WebApplication app)
    {
        var chambersGroup = app
            .MapGroup("chambers")
            .WithTags("chambers");

        chambersGroup.MapGet("", ()
            => {});
        
        chambersGroup.MapPost("", ()
            => {});
        
        return chambersGroup;
    }
}