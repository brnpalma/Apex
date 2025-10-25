using Apex.Shared.Settings;
using AuthApex.Application.Common.Interfaces;
using AuthApex.Application.UseCases.Autenticacao.Services;
using AuthApex.Domain.Entities;
using AuthApex.Infrastructure.Persistence;
using AuthApex.Infrastructure.Repositories;
using AuthApex.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthApex.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("AutenticacaoApexDb");
        Guard.Against.Null(connectionString, message: "Connection string 'AutenticacaoApexDb' não encontrada.");

        builder.Services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        builder.Services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        builder.Services.AddAuthorizationBuilder();

        builder.Services.AddSingleton(TimeProvider.System);
    }
}
