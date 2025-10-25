using Apex.Shared.Results;
using Moq;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Entregas;
using TransportApex.Application.UseCases.Entregas.RegistrarEntrega;

namespace TransportApex.Application.UnitTests
{
    [TestClass]
    public class RegistrarEntregaHandlerTests
    {
        private Mock<IEntregaService> _entregaService = null!;
        private RegistrarEntregaHandler _handler = null!;

        [TestInitialize]
        public void Setup()
        {
            _entregaService = new Mock<IEntregaService>();
            _handler = new RegistrarEntregaHandler(_entregaService.Object);
        }

        [TestMethod]
        public async Task Handle_ChamaServicoERetornaResultado()
        {
            var expected = Result<EntregaDto>.Ok(new EntregaDto { Id = 1, Sucesso = true }, 201);
            _entregaService.Setup(s => s.RegistrarEntregaAsync(It.IsAny<long>(), It.IsAny<long>()))
                .ReturnsAsync(expected);

            var request = new RegistrarEntregaRequest(FornecedorId: 1, ProdutoId: 2);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Sucesso);
            Assert.AreEqual(201, result.Status);

            _entregaService.Verify(s => s.RegistrarEntregaAsync(1, 2), Times.Once);
        }
    }
}