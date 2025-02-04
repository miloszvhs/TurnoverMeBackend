using Microsoft.Extensions.DependencyInjection;
using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Infrastructure.DAL.Decorators;
using TurnoverMeBackend.Infrastructure.DAL.Handlers;
using TurnoverMeBackend.Infrastructure.DAL.Repositories;

namespace TurnoverMeBackend.Infrastructure;

public static class ModuleInjecter
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        
        services.Scan(scan => scan
            .FromAssemblyOf<GetInvoicesHandler>()
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceNumberRepository, InvoiceNumberRepository>();
        
        return services;
    }
}