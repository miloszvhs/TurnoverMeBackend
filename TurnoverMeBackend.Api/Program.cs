using System.Transactions;
using TurnoverMeBackend.Application;
using TurnoverMeBackend.Config;
using TurnoverMeBackend.Config.Configs;
using TurnoverMeBackend.Api.Endpoints;
using TurnoverMeBackend.Infrastructure;
using TurnoverMeBackend.Infrastructure.DAL;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TurnoverMeBackend.Domain.Entities;

namespace TurnoverMeBackend.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(o =>
        {
            // o.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
            // {
            //     In = ParameterLocation.Header,
            //     Description = "Enter proper JWT token",
            //     Name = "Authorization",
            //     Scheme = "Bearer",
            //     BearerFormat = "JWT",
            //     Type = SecuritySchemeType.Http
            // });
            //
            // o.AddSecurityRequirement(new OpenApiSecurityRequirement
            // {
            //     {
            //         new OpenApiSecurityScheme
            //         {
            //             Reference = new OpenApiReference
            //             {
            //                 Type = ReferenceType.SecurityScheme,
            //                 Id = "BearerAuth"
            //             }
            //         },
            //         Array.Empty<string>()
            //     }
            // });
        });

        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policyBuilder =>
            {
                policyBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
            });
        });

        builder.Services.AddDbContext<TurnoverMeDbContext>(opt =>
        {
            var dbConfig = builder.Configuration.GetRequiredSection(DbConfig.Node).Get<DbConfig>();
            opt.UseNpgsql(dbConfig.DatabaseConnectionString);      
            
        });

        builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<TurnoverMeDbContext>();

        builder.Services.ConfigureAll<BearerTokenOptions>(option =>
        {
            option.BearerTokenExpiration = TimeSpan.FromMinutes(1);
        });

        //builder.Services.AddAuthorization();

        builder.Services.AddUglyServices();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();
        builder.Services.InitializeConfiguration(builder);
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<TransactionMiddleware>();

        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        //app.UseAuthorization();

        app.AddIdentityEndpoints();
        app.MapInvoiceEndpoints();
        app.AddChambersEndpoints();
        app.AddWorkflowEndpoints();
        app.MapInvoiceApprovalEndpoints();
        app.MapGroupEndpoints();
        app.AddAdminEndpoints();//.RequireAuthorization(x => x.RequireRole("Admin"));

        app.Run();
    }
}

public class TransactionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TransactionMiddleware> _logger;

    public TransactionMiddleware(RequestDelegate next, ILogger<TransactionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, TurnoverMeDbContext dbContext)
    {
        if (dbContext.Database.CurrentTransaction != null)
        {
            _logger.LogInformation("Istnieje już aktywna transakcja, kontynuuję...");
            await _next(context);
            return;
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            _logger.LogInformation("Transakcja rozpoczęta.");
            await _next(context);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            _logger.LogInformation("Transakcja zatwierdzona.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Błąd! Wycofuję transakcję.");
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            if (dbContext.Database.CurrentTransaction != null)
            {
                _logger.LogWarning("Transakcja nadal otwarta! Zakończam ją.");
                await dbContext.Database.CurrentTransaction.RollbackAsync();
            }
        }
    }
}

