using AuthApex.Domain.ValueObjects;

namespace AuthApex.Domain.UnitTests
{
    [TestClass]
    public class SenhaTests
    {
        [TestMethod]
        public void TryCreate_Invalida_RetornaFalse()
        {
            var ok = Senha.TryCreate("short", out var senha, out var error);
            Assert.IsFalse(ok);
            Assert.IsNull(senha);
            Assert.IsNotNull(error);
        }

        [TestMethod]
        public void TryCreate_Valida_VerificarFunciona()
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