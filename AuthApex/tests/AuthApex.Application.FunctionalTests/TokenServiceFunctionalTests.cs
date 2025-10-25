using Microsoft.Extensions.Options;
using AuthApex.Infrastructure.Services;
using AuthApex.Domain.Entities;
using AuthApex.Domain.ValueObjects;
using Apex.Shared.Settings;
using Apex.Shared.Enums;

namespace AuthApex.Application.FunctionalTests
{
    [TestClass]
    public class TokenServiceFunctionalTests
    {
        private TokenService _tokenService = null!;

        [TestInitialize]
        public void Init()
        {
            var jwt = new JwtSettings { SecretKey = "verylong_test_secret_key_1234567890" };
            var options = Options.Create(jwt);
            _tokenService = new TokenService(options);
        }

        [TestMethod]
        public void GerarToken_Then_ValidarToken_ReturnsTrue()
        {
            _ = Email.TryCreate("u@e.com", out var email, out _);
            _ = Senha.TryCreate("Abcdef1!", out var senha, out _);

            var usuario = new Usuario
            {
                Id = 1,
                Email = email!,
                SenhaHash = senha!,
                Role = Role.User
            };

            var token = _tokenService.GerarToken(usuario);
            var valido = _tokenService.ValidarToken(token);

            Assert.IsFalse(string.IsNullOrWhiteSpace(token));
            Assert.IsTrue(valido);
        }

        [TestMethod]
        public void ValidarToken_InvalidToken_ReturnsFalse()
        {
            var valido = _tokenService.ValidarToken("algum.token.invalido");
            Assert.IsFalse(valido);
        }
    }
}