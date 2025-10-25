using Moq;
using Apex.Shared.Enums;
using Apex.Shared.Interfaces;
using Apex.Shared.Results;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Entregas;
using TransportApex.Application.UseCases.Entregas.RegistrarEntrega;

namespace TransportApex.Application.UnitTests
{
    [TestClass]
    public class RegistrarEntregaHandlerTests
    {
        private Mock<IEntregaService> _entregaService = null!;
        private Mock<ICurrentUserService> _currentUserService = null!;
        private RegistrarEntregaHandler _handler = null!;

        [TestInitialize]
        public void Setup()
        {
            _entregaService = new Mock<IEntregaService>();
            _currentUserService = new Mock<ICurrentUserService>();
            _currentUserService.SetupGet(c => c.IdUsuario).Returns("user-1");
            _currentUserService.SetupGet(c => c.Role).Returns(Role.Admin);

            _handler = new RegistrarEntregaHandler(_entregaService.Object, _currentUserService.Object);
        }

        [TestMethod]
        public async Task Handle_ChamaServicoERetornaResultado()
        {
            var expected = Result<EntregaDto>.Ok(new EntregaDto { Id = 1, Sucesso = true }, 201);
            _entregaService.Setup(s => s.RegistrarEntregaAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<string>(), It.IsAny<Role?>()))
                .ReturnsAsync(expected);

            var request = new RegistrarEntregaRequest(FornecedorId: 1, ProdutoId: 2);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Sucesso);
            Assert.AreEqual(201, result.Status);

            _entregaService.Verify(s => s.RegistrarEntregaAsync(1, 2, "user-1", Role.Admin), Times.Once);
        }
    }
}