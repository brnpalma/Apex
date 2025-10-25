using Apex.Shared.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.UseCases.Entregas.Services;
using TransportApex.Application.UseCases.Fornecedores.Services;
using TransportApex.Infrastructure.Persistence;
using TransportApex.Infrastructure.Repositories;

namespace TransportApex.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("TransportApexDb");
        Guard.Against.Null(connectionString, message: "Connection string 'TransportApexDb' não encontrada.");

        builder.Services.AddDbContext<TransportDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

        builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
        builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
        builder.Services.AddScoped<IEntregaRepository, EntregaRepository>();
        builder.Services.AddScoped<IFornecedorService, FornecedorService>();
        builder.Services.AddScoped<IProdutoService, ProdutoService>();
        builder.Services.AddScoped<IEntregaService, EntregaService>();

        //builder.Services.AddAuthentication()
        //    .AddBearerToken(IdentityConstants.BearerScheme);

        builder.Services.AddAuthorizationBuilder();

        builder.Services.AddSingleton(TimeProvider.System);
    }
}
