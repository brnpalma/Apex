using Microsoft.Extensions.Options;
using AuthApex.Infrastructure.Services;
using AuthApex.Application.Common.Interfaces;
using AuthApex.Domain.Entities;
using AuthApex.Domain.ValueObjects;
using Apex.Shared.Settings;

namespace AuthApex.Infrastructure.IntegrationTests
{
    [TestClass]
    public class TokenServiceTests
    {
        private ITokenService _tokenService = null!;

        [TestInitialize]
        public void Setup()
        {
            var jwt = new JwtSettings { SecretKey = "verylong_test_secret_key_1234567890" };
            var options = Options.Create(jwt);
            _tokenService = new TokenService(options);
        }

        [TestMethod]
        public void GerarToken_Valido_ValidarTokenRetornaTrue()
        {
            // Arrange
            _ = Email.TryCreate("u@e.com", out var email, out _);
            _ = Senha.TryCreate("Abcdef1!", out var senha, out _);
            var usuario = new Usuario
            {
                Id = 10,
                Email = email!,
                SenhaHash = senha!,
                Role = Apex.Shared.Enums.Role.User
            };

            // Act
            var token = _tokenService.GerarToken(usuario);
            var valido = _tokenService.ValidarToken(token);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(token));
            Assert.IsTrue(valido);
        }

        [TestMethod]
        public void ValidarToken_TokenInvalido_RetornaFalse()
        {
            var invalido = _tokenService.ValidarToken("algum.token.invalido");
            Assert.IsFalse(invalido);
        }
    }
}