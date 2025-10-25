using AuthApex.Domain.ValueObjects;

namespace AuthApex.Domain.UnitTests
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void TryCreate_Vazio_RetornaFalse()
        {
            var ok = Email.TryCreate("", out var email, out var error);
            Assert.IsFalse(ok);
            Assert.IsNull(email);
            Assert.IsNotNull(error);
        }

        [TestMethod]
        public void TryCreate_FormatoInvalido_RetornaFalse()
        {
            var ok = Email.TryCreate("invalido@", out var email, out var error);
            Assert.IsFalse(ok);
            Assert.IsNull(email);
            Assert.IsNotNull(error);
        }

        [TestMethod]
        public void TryCreate_Valido_RetornaTrue()
        {
            var ok = Email.TryCreate("usuario@exemplo.com", out var email, out var error);
            Assert.IsTrue(ok);
            Assert.IsNotNull(email);
            Assert.IsNull(error);
            Assert.AreEqual("usuario@exemplo.com", email!.Endereco);
            Assert.AreEqual("usuario@exemplo.com", email.ToString());
        }
    }
}