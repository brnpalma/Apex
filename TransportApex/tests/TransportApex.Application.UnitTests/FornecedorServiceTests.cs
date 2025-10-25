using Moq;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.UseCases.Fornecedores.Services;
using TransportApex.Domain.Entities;
using TransportApex.Domain.ValueObjects;

namespace TransportApex.Application.UnitTests
{
    [TestClass]
    public class FornecedorServiceTests
    {
        private Mock<IFornecedorRepository> _fornecedorRepo = null!;
        private FornecedorService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _fornecedorRepo = new Mock<IFornecedorRepository>();
            _service = new FornecedorService(_fornecedorRepo.Object);
        }

        [TestMethod]
        public async Task CadastrarAsync_CnpjJaExiste_Retorna400()
        {
            _fornecedorRepo.Setup(r => r.ExistePorCnpjAsync(It.IsAny<string>())).ReturnsAsync(true);

            var result = await _service.CadastrarAsync("Nome", "00.000.000/0000-00");

            Assert.IsFalse(result.Sucesso);
            Assert.AreEqual(400, result.Status);
        }

        [TestMethod]
        public async Task CadastrarAsync_CnpjInvalido_Retorna400()
        {
            _fornecedorRepo.Setup(r => r.ExistePorCnpjAsync(It.IsAny<string>())).ReturnsAsync(false);

            var result = await _service.CadastrarAsync("Nome", "123");

            Assert.IsFalse(result.Sucesso);
            Assert.AreEqual(400, result.Status);
        }

        [TestMethod]
        public async Task CadastrarAsync_Sucesso_Retorna201EChamaRepositorio()
        {
            _fornecedorRepo.Setup(r => r.ExistePorCnpjAsync(It.IsAny<string>())).ReturnsAsync(false);
            _fornecedorRepo.Setup(r => r.AdicionarAsync(It.IsAny<Fornecedor>()))
                .Callback<Fornecedor>(f => f.Id = 77)
                .Returns(Task.CompletedTask);

            var cnpjRaw = "11222333000181";
            var result = await _service.CadastrarAsync("Fornecedor Z", cnpjRaw);

            Assert.IsTrue(result.Sucesso);
            Assert.AreEqual(201, result.Status);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(77, result.Data!.Id);

            _fornecedorRepo.Verify(r => r.AdicionarAsync(It.IsAny<Fornecedor>()), Times.Once);
        }

        [TestMethod]
        public async Task ListarAsync_RetornaMapaCorreto()
        {
            var f1 = new Fornecedor("F1", Cnpj.TryCreate("11222333000181", out var c1, out _) ? c1! : default!) { Id = 1 };
            var f2 = new Fornecedor("F2", Cnpj.TryCreate("22233344000199", out var c2, out _) ? c2! : default!) { Id = 2 };

            _fornecedorRepo.Setup(r => r.ObterTodosAsync()).ReturnsAsync(new List<Fornecedor> { f1, f2 });

            var result = await _service.ListarAsync();

            Assert.IsTrue(result.Sucesso);
            Assert.AreEqual(200, result.Status);
            var list = result.Data!.ToList();
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("F1", list[0].Nome);
        }
    }
}