namespace TurnoverMeBackend.Api.Endpoints;

public static class ApprovalEndpoints
{
    public static RouteGroupBuilder AddApprovalEndpoints(this WebApplication app)
    {
        var chambersGroup = app
            .MapGroup("approval")
            .WithTags("approval");

        chambersGroup.MapGet("", ()
            => {});
        
        chambersGroup.MapPost("", ()
            => {});
        
        chambersGroup.MapPost("{invoiceId}/approve/{stageLevel}", ()
            => {});
        
        chambersGroup.MapPost("{invoiceId}/reject/{stageLevel}", ()
            => {});
        
        return chambersGroup;
    }
}