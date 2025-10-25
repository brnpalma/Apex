using Moq;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.UseCases.Entregas.Services;
using TransportApex.Domain.Entities;
using Apex.Shared.Enums;

namespace TransportApex.Application.UnitTests
{
    [TestClass]
    public class EntregaServiceTests
    {
        private Mock<IEntregaRepository> _entregaRepo = null!;
        private Mock<IFornecedorRepository> _fornecedorRepo = null!;
        private Mock<IProdutoRepository> _produtoRepo = null!;
        private EntregaService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _entregaRepo = new Mock<IEntregaRepository>();
            _fornecedorRepo = new Mock<IFornecedorRepository>();
            _produtoRepo = new Mock<IProdutoRepository>();

            _service = new EntregaService(_entregaRepo.Object, _fornecedorRepo.Object, _produtoRepo.Object);
        }

        [TestMethod]
        public async Task RegistrarEntregaAsync_FornecedorNaoEncontrado_Retorna404()
        {
            _fornecedorRepo.Setup(r => r.ObterPorIdAsync(It.IsAny<long>())).ReturnsAsync((Fornecedor?)null);

            var result = await _service.RegistrarEntregaAsync(1, 1, "1", Role.Admin);

            Assert.IsFalse(result.Sucesso);
            Assert.AreEqual(404, result.Status);
        }

        [TestMethod]
        public async Task RegistrarEntregaAsync_ProdutoNaoEncontrado_Retorna404()
        {
            var fornecedor = new Fornecedor("Forn", default!);
            _fornecedorRepo.Setup(r => r.ObterPorIdAsync(It.IsAny<long>())).ReturnsAsync(fornecedor);
            _produtoRepo.Setup(r => r.ObterPorIdAsync(It.IsAny<long>())).ReturnsAsync((Produto?)null);

            var result = await _service.RegistrarEntregaAsync(1, 2, "1", Role.Admin);

            Assert.IsFalse(result.Sucesso);
            Assert.AreEqual(404, result.Status);
        }

        [TestMethod]
        public async Task RegistrarEntregaAsync_Sucesso_Retorna201EChamaRepositorio()
        {
            var fornecedor = new Fornecedor("Fornecedor X", default!) { Id = 11 };
            var produto = new Produto("Produto Y", 1.0m) { Id = 22 };

            _fornecedorRepo.Setup(r => r.ObterPorIdAsync(It.IsAny<long>())).ReturnsAsync(fornecedor);
            _produtoRepo.Setup(r => r.ObterPorIdAsync(It.IsAny<long>())).ReturnsAsync(produto);

            _entregaRepo.Setup(r => r.AdicionarAsync(It.IsAny<Entrega>()))
                .Callback<Entrega>(e => e.Id = 100)
                .Returns(Task.CompletedTask);

            var result = await _service.RegistrarEntregaAsync(fornecedor.Id, produto.Id, "user-1", Role.Admin);

            Assert.IsTrue(result.Sucesso);
            Assert.AreEqual(201, result.Status);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(100, result.Data!.Id);
            Assert.AreEqual(fornecedor.Id, result.Data!.FornecedorId);
            Assert.AreEqual(produto.Id, result.Data!.ProdutoId);

            _entregaRepo.Verify(r => r.AdicionarAsync(It.IsAny<Entrega>()), Times.Once);
        }

        [TestMethod]
        public async Task ListarAsync_RetornaMapaCorreto()
        {
            var f1 = new Fornecedor("F1", default!) { Id = 1 };
            var p1 = new Produto("P1", 0.5m) { Id = 10 };
            var e1 = new Entrega(f1, p1) { Id = 5 };

            var f2 = new Fornecedor("F2", default!) { Id = 2 };
            var p2 = new Produto("P2", 2m) { Id = 20 };
            var e2 = new Entrega(f2, p2) { Id = 6 };

            _entregaRepo.Setup(r => r.ObterTodasAsync()).ReturnsAsync(new List<Entrega> { e1, e2 });

            var result = await _service.ListarAsync("user-1", Role.Admin);

            Assert.IsTrue(result.Sucesso);
            Assert.AreEqual(200, result.Status);
            Assert.IsNotNull(result.Data);
            var list = result.Data!.ToList();
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(5, list[0].Id);
            Assert.AreEqual("P2", list[1].DescricaoProduto);
        }
    }
}