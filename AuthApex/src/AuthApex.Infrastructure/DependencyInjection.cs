using AuthApex.Application.Auth.Services;
using AuthApex.Application.Common.Interfaces;
using AuthApex.Domain.Constants;
using AuthApex.Domain.Entities;
using AuthApex.Infrastructure.Data.Interceptors;
using AuthApex.Infrastructure.Persistence;
using AuthApex.Infrastructure.Repositories;
using AuthApex.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthApex.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("ApexDb");
        Guard.Against.Null(connectionString, message: "Connection string 'ApexDb' not found.");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        builder.Services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        builder.Services.AddAuthorizationBuilder();

        builder.Services.AddSingleton(TimeProvider.System);

        builder.Services.AddAuthorization(options =>
            options.AddPolicy( Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));
    }
}
