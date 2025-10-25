using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.UseCases.Entregas.Services;
using TransportApex.Application.UseCases.Fornecedores.Services;
using TransportApex.Application.UseCases.Produtos.Services;

namespace TransportApex.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProdutoService, ProdutoService>();
        builder.Services.AddScoped<IEntregaService, EntregaService>();
        builder.Services.AddScoped<IFornecedorService, FornecedorService>();

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
    }
}
