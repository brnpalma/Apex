using TransportApex.Domain.Entities;

namespace TransportApex.Domain.UnitTests
{
    [TestClass]
    public class ProdutoTests
    {
        [TestMethod]
        public void TryCreate_Invalido_RetornaFalse()
        {
            var ok = Produto.TryCreate("", 0m, out var produto, out var error);
            Assert.IsFalse(ok);
            Assert.IsNull(produto);
            Assert.IsNotNull(error);
        }

        [TestMethod]
        public void TryCreate_Valido_RetornaTrue()
        {
            var ok = Produto.TryCreate("Produto válido", 1.25m, out var produto, out var error);
            Assert.IsTrue(ok);
            Assert.IsNotNull(produto);
            Assert.IsNull(error);
        }
    }
}