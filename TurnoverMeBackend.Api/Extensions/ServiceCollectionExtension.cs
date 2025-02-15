using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using TurnoverMeBackend.Config.Configs;
using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Infrastructure.DAL;

namespace TurnoverMeBackend.Api.Extensions;

public static class ServiceCollectionExtension
{
	public static IServiceCollection AddConfiguration(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<TurnoverMeDbContext>()
            .AddDefaultTokenProviders();
        
        services.AddSwaggerGen(o =>
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

        
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policyBuilder =>
            {
                policyBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
            });
        });

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = GetTokenValidationParams(
                    Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:SecretKey").Value));
            });
        
        return services;
    }

    private static TokenValidationParameters GetTokenValidationParams(byte[] key)
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    } 
} 
