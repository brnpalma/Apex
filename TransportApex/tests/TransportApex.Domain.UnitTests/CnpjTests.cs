using TransportApex.Domain.ValueObjects;

namespace TransportApex.Domain.UnitTests
{
    [TestClass]
    public class CnpjTests
    {
        [TestMethod]
        public void TryCreate_Vazio_RetornaFalse()
        {
            var ok = Cnpj.TryCreate("", out var cnpj, out var error);
            Assert.IsFalse(ok);
            Assert.IsNull(cnpj);
            Assert.IsNotNull(error);
        }

        [TestMethod]
        public void TryCreate_TamanhoInvalido_RetornaFalse()
        {
            var ok = Cnpj.TryCreate("123", out var cnpj, out var error);
            Assert.IsFalse(ok);
            Assert.IsNull(cnpj);
            Assert.IsNotNull(error);
        }

        [TestMethod]
        public void TryCreate_Valido_RetornaTrueEFormatado()
        {
            var raw = "11222333000181";
            var ok = Cnpj.TryCreate(raw, out var cnpj, out var error);
            Assert.IsTrue(ok);
            Assert.IsNotNull(cnpj);
            Assert.IsTrue(cnpj!.Numero.Contains(".") && cnpj.Numero.Contains("/"));
        }
    }
}