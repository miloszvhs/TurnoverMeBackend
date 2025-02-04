using Microsoft.Extensions.DependencyInjection;
using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.Commands;
using TurnoverMeBackend.Application.Commands.Handlers;
using TurnoverMeBackend.Application.Services;

namespace TurnoverMeBackend.Application;

public static class ModuleInjecter
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateInvoiceCommand>, CreateInvoiceCommandHandler>();
        services.AddScoped<IAesService, AesService>();
        services.AddScoped<IInvoiceNumberGenerationService, InvoiceNumberGenerationService>();
        return services;
    }
    
}