using Moq;
using Apex.Shared.Results;
using TransportApex.Application.UseCases.Entregas.RegistrarEntrega;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Entregas;

namespace TransportApex.Application.FunctionalTests
{
    [TestClass]
    public class RegistrarEntregaHandlerFunctionalTests
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
        public async Task Handle_DelegaParaServico_RetornaServiceResult()
        {
            var expected = Result<EntregaDto>.Ok(new EntregaDto { Id = 10, Sucesso = true }, 201);
            _entregaService.Setup(s => s.RegistrarEntregaAsync(It.IsAny<long>(), It.IsAny<long>()))
                .ReturnsAsync(expected);

            var request = new RegistrarEntregaRequest(1, 2);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Sucesso);
            Assert.AreEqual(201, result.Status);
            _entregaService.Verify(s => s.RegistrarEntregaAsync(1, 2), Times.Once);
        }
    }
}