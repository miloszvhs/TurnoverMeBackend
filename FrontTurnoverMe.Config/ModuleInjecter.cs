﻿using FrontTurnoverMe.Config.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FrontTurnoverMe.Config;

public static class ModuleInjecter
{
    public static void InitializeConfiguration(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        serviceCollection.AddOptions<DbConfig>()
            .Bind(builder.Configuration.GetSection(DbConfig.Node));
    }}