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

namespace TurnoverMeBackend.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(o =>
        {
            o.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter proper JWT token",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Type = SecuritySchemeType.Http
            });
    
            o.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "BearerAuth"
                        }
                    },
                    Array.Empty<string>()
                }
            });
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

        builder.Services.AddDbContext<InvoicesDbContext>(opt =>
        {
            var dbConfig = builder.Configuration.GetRequiredSection(DbConfig.Node).Get<DbConfig>();
            opt.UseNpgsql(dbConfig.DatabaseConnectionString);        
        });

        builder.Services.AddIdentityApiEndpoints<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<TurnoverMeDbContext>();

        builder.Services.ConfigureAll<BearerTokenOptions>(option =>
        {
            option.BearerTokenExpiration = TimeSpan.FromMinutes(1);
        });

        builder.Services.AddAuthorization();

        builder.Services.AddInfrastructure();
        builder.Services.AddApplication();
        builder.Services.InitializeConfiguration(builder);
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.AddIdentityEndpoints();
        app.MapInvoiceEndpoints();

        app.Run();
    }
}