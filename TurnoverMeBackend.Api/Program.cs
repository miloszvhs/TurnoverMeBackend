using TurnoverMeBackend.Application;
using TurnoverMeBackend.Config;
using TurnoverMeBackend.Config.Configs;
using TurnoverMeBackend.Api.Endpoints;
using TurnoverMeBackend.Infrastructure;
using TurnoverMeBackend.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TurnoverMeBackend.Api.Extensions;

namespace TurnoverMeBackend.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();
        
        builder.Services.AddDbContext<TurnoverMeDbContext>(opt =>
        {
            var dbConfig = builder.Configuration.GetRequiredSection(DbConfig.Node).Get<DbConfig>();
            opt.UseNpgsql(dbConfig.DatabaseConnectionString, opt => opt.CommandTimeout(60));      
        });
       
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddConfiguration(builder.Configuration);
        builder.Services.AddApiServices();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();
        builder.Services.InitializeConfiguration(builder);
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        //app.UseMiddleware<TransactionMiddleware>();
        
        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.AddCustomIdentityEndpoints();
        app.MapInvoiceEndpoints().RequireAuthorization();
        app.AddChambersEndpoints().RequireAuthorization();
        app.AddWorkflowEndpoints().RequireAuthorization();
        app.MapInvoiceApprovalEndpoints().RequireAuthorization();
        app.MapGroupEndpoints().RequireAuthorization();
        app.AddAdminEndpoints().RequireAuthorization();

        app.Run();
    }
}