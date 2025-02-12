using TurnoverMeBackend.Api.UglyServices;

namespace TurnoverMeBackend.Api;

public static class ModuleInitializer
{
    public static void AddUglyServices(this IServiceCollection services)
    {
        var baseServiceType = typeof(BaseService);
        var assembly = baseServiceType.Assembly;

        var serviceTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(baseServiceType));

        foreach (var serviceType in serviceTypes)
            services.AddScoped(serviceType);

        services.AddScoped<GroupRepositoryy>();
    }
}