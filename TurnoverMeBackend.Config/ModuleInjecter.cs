using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TurnoverMeBackend.Config.Configs;

namespace TurnoverMeBackend.Config;

public static class ModuleInjecter
{
    public static void InitializeConfiguration(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        serviceCollection.AddOptions<DbConfig>()
            .Bind(builder.Configuration.GetSection(DbConfig.Node));       
        
        serviceCollection.AddOptions<JwtSettings>()
            .Bind(builder.Configuration.GetSection(JwtSettings.Node));
    }}