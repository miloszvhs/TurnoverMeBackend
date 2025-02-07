namespace TurnoverMeBackend.Api.Endpoints;

public static class ChambersEndpoints
{
    public static WebApplication AddChambersEndpoints(this WebApplication app)
    {
        var chambersGroup = app
            .MapGroup("chambers")
            .WithTags("chambers")
            .RequireAuthorization(x => x.RequireRole("User"));

        chambersGroup.MapGet("/chambers", ()
            => {});
        
        return app;
    }
}