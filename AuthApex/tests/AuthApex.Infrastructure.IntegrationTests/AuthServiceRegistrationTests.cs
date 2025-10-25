using AuthApex.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthApex.Infrastructure.IntegrationTests
{
    [TestClass]
    public class AuthServiceRegistrationTests
    {
        [TestMethod]
        public void AddInfrastructureServices_RegistraIAuthService()
        {
            var builder = new HostApplicationBuilder();
            builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:AutenticacaoApexDb", "Server=(local);Database=AuthTest;Trusted_Connection=True;" },
                { "JwtSettings:SecretKey", "verylong_test_secret_key_1234567890" }
            });

            DependencyInjection.AddInfrastructureServices(builder);

            var provider = builder.Services.BuildServiceProvider();

            var authService = provider.GetService(typeof(IAuthService));
            Assert.IsNotNull(authService, "IAuthService deve estar registrado. Se falhar, verifique AddInfrastructureServices.");
        }
    }
}