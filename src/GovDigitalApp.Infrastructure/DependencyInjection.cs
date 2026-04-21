using GovDigitalApp.Application.Auth;
using GovDigitalApp.Application.Common;
using GovDigitalApp.Application.Documents;
using GovDigitalApp.Application.Dvla;
using GovDigitalApp.Application.Identity;
using GovDigitalApp.Infrastructure.Persistence;
using GovDigitalApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
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
        var keyRingPath = Environment.GetEnvironmentVariable("DP_KEY_RING")
            ?? Path.Combine(AppContext.BaseDirectory, "dp-keys");
        Directory.CreateDirectory(keyRingPath);
        services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(keyRingPath))
            .SetApplicationName("GovDigitalApp");

        if (dbContextOptions != null)
        {
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                dbContextOptions(options);
            });
        }
        else
        {
            var connection = Environment.GetEnvironmentVariable("DB_CONNECTION")
                ?? configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DB connection string not configured");
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.UseSqlServer(connection)
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
                var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY")
                    ?? configuration["Jwt:Key"]
                    ?? throw new InvalidOperationException("JWT key not configured");
                if (jwtKey.Length < 32)
                    throw new InvalidOperationException("JWT key must be at least 32 characters for HS256.");
                options.RequireHttpsMetadata = !string.Equals(
                    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "Development", StringComparison.OrdinalIgnoreCase);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromMinutes(1),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                };
            });

        return services;
    }
}
