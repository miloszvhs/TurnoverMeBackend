using FrontTurnoverMe.Application.Abstractions;
using FrontTurnoverMe.Application.DTO;
using FrontTurnoverMe.Application.Interfaces;
using FrontTurnoverMe.Application.Queries;
using FrontTurnoverMe.Infrastructure.DAL.Decorators;
using FrontTurnoverMe.Infrastructure.DAL.Handlers;
using FrontTurnoverMe.Infrastructure.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FrontTurnoverMe.Infrastructure;

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