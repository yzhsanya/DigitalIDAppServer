using GovDigitalApp.Application.Auth;
using GovDigitalApp.Application.Common;
using GovDigitalApp.Application.Documents;
using GovDigitalApp.Application.Dvla;
using GovDigitalApp.Application.Identity;
using GovDigitalApp.Infrastructure.Persistence;
using GovDigitalApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GovDigitalApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<DbContextOptionsBuilder>? dbContextOptions = null)
    {
        if (dbContextOptions != null)
        {
            services.AddDbContext<AppDbContext>(dbContextOptions);
        }
        else
        {
            var isRailway = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("RAILWAY_ENVIRONMENT"));
            services.AddDbContext<AppDbContext>(options =>
            {
                if (isRailway)
                    options.UseSqlite("Data Source=/tmp/govdigitalapp.db")
                           .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
                else
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                           .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            });
        }

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IDocumentsService, DocumentsService>();
        services.AddScoped<IDvlaService, DvlaService>();
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not configured");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                };
            });

        return services;
    }
}
