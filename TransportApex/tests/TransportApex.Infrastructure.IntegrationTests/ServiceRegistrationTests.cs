using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TransportApex.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace TransportApex.Infrastructure.IntegrationTests
{
    [TestClass]
    public class ServiceRegistrationTests
    {
        [TestMethod]
        public void AddInfrastructureServices_RegistraServicosEsperados()
        {
            var builder = new HostApplicationBuilder();
            builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:TransportApexDb", "Server=(local);Database=TransportTest;Trusted_Connection=True;" }
            });

            DependencyInjection.AddInfrastructureServices(builder);

            var provider = builder.Services.BuildServiceProvider();

            Assert.IsNotNull(provider.GetService(typeof(IFornecedorRepository)));
            Assert.IsNotNull(provider.GetService(typeof(IProdutoRepository)));
            Assert.IsNotNull(provider.GetService(typeof(IEntregaRepository)));
            Assert.IsNotNull(provider.GetService(typeof(IFornecedorService)));
            Assert.IsNotNull(provider.GetService(typeof(IProdutoService)));
            Assert.IsNotNull(provider.GetService(typeof(IEntregaService)));
        }
    }
}