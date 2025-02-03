using FrontTurnoverMe.Application.Abstractions;
using FrontTurnoverMe.Application.Commands;
using FrontTurnoverMe.Application.Commands.Handlers;
using FrontTurnoverMe.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FrontTurnoverMe.Application;

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