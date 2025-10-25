using Moq;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.UseCases.Produtos.Services;
using TransportApex.Domain.Entities;
using Apex.Shared.Enums;

namespace TransportApex.Application.UnitTests
{
    [TestClass]
    public class ProdutoServiceTests
    {
        private const string AdminUserId = "test-admin";
        private const Role AdminRole = Role.Admin;

        private Mock<IProdutoRepository> _produtoRepo = null!;
        private ProdutoService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _produtoRepo = new Mock<IProdutoRepository>();
            _service = new ProdutoService(_produtoRepo.Object);
        }

        [TestMethod]
        public async Task CadastrarAsync_ExistePorNome_Retorna400()
        {
            _produtoRepo.Setup(r => r.ExistePorNomeAsync(It.IsAny<string>())).ReturnsAsync(true);

            var result = await _service.CadastrarAsync("Nome", 1m, AdminUserId, AdminRole);

            Assert.IsFalse(result.Sucesso);
            Assert.AreEqual(400, result.Status);
        }

        [TestMethod]
        public async Task CadastrarAsync_ProdutoInvalido_Retorna400()
        {
            _produtoRepo.Setup(r => r.ExistePorNomeAsync(It.IsAny<string>())).ReturnsAsync(false);

            var result = await _service.CadastrarAsync("", 0m, AdminUserId, AdminRole);

            Assert.IsFalse(result.Sucesso);
            Assert.AreEqual(400, result.Status);
        }

        [TestMethod]
        public async Task CadastrarAsync_Sucesso_Retorna201EChamaRepositorio()
        {
            _produtoRepo.Setup(r => r.ExistePorNomeAsync(It.IsAny<string>())).ReturnsAsync(false);
            _produtoRepo.Setup(r => r.AdicionarAsync(It.IsAny<Produto>()))
                .Callback<Produto>(p => p.Id = 99)
                .Returns(Task.CompletedTask);

            var result = await _service.CadastrarAsync("Produto Teste", 1.5m, AdminUserId, AdminRole);

            Assert.IsTrue(result.Sucesso);
            Assert.AreEqual(201, result.Status);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(99, result.Data!.Id);

            _produtoRepo.Verify(r => r.AdicionarAsync(It.IsAny<Produto>()), Times.Once);
        }

        [TestMethod]
        public async Task ListarAsync_RetornaMapaCorreto()
        {
            var p1 = new Produto("P1", 1m) { Id = 1 };
            var p2 = new Produto("P2", 2m) { Id = 2 };

            _produtoRepo.Setup(r => r.ObterTodosAsync()).ReturnsAsync(new List<Produto> { p1, p2 });

            var result = await _service.ListarAsync(AdminUserId, AdminRole);

            Assert.IsTrue(result.Sucesso);
            Assert.AreEqual(200, result.Status);
            var list = result.Data!.ToList();
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("P1", list[0].Descricao);
            Assert.AreEqual(2, list[1].Id);
        }
    }
}