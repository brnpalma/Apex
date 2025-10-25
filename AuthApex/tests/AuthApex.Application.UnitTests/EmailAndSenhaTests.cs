using AuthApex.Domain.ValueObjects;

namespace AuthApex.Application.UnitTests
{
    [TestClass]
    public class EmailAndSenhaTests
    {
        [TestMethod]
        public void Email_TryCreate_Vazio_RetornaFalse()
        {
            var ok = Email.TryCreate("", out var email, out var error);
            Assert.IsFalse(ok);
            Assert.IsNull(email);
            Assert.IsNotNull(error);
        }

        [TestMethod]
        public void Email_TryCreate_Valido_RetornaTrue()
        {
            var ok = Email.TryCreate("usuario@exemplo.com", out var email, out var error);
            Assert.IsTrue(ok);
            Assert.IsNotNull(email);
            Assert.IsNull(error);
            Assert.AreEqual("usuario@exemplo.com", email!.Endereco);
        }

        [TestMethod]
        public void Senha_TryCreate_Invalida_RetornaFalse()
        {
            var ok = Senha.TryCreate("short", out var senha, out var error);
            Assert.IsFalse(ok);
            Assert.IsNull(senha);
            Assert.IsNotNull(error);
        }

        [TestMethod]
        public void Senha_TryCreate_Valida_VerificarFunciona()
        {
            var raw = "Abcdef1!";
            var ok = Senha.TryCreate(raw, out var senha, out var error);
            Assert.IsTrue(ok);
            Assert.IsNotNull(senha);
            Assert.IsTrue(senha!.Verificar(raw));
            Assert.AreEqual("********", senha.ToString());
        }
    }
}