using FrontTurnoverMe.Application;
using FrontTurnoverMe.Config;
using FrontTurnoverMe.Config.Configs;
using FrontTurnoverMe.Domain.Entities;
using FrontTurnoverMe.Domain.Entities.Invoices;
using FrontTurnoverMe.Endpoints;
using FrontTurnoverMe.Infrastructure;
using FrontTurnoverMe.Infrastructure.DAL;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace FrontTurnoverMe;

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

        app.MapIdentityApi<IdentityUser>()
            .WithTags("Authentication");
        app.MapInvoiceEndpoints();

        app.Run();
    }
}