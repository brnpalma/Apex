using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Entregas;

namespace TransportApex.Application.UseCases.Entregas.RegistrarEntrega
{
    public class RegistrarEntregaHandler(IEntregaService entregaService) : IRequestHandler<RegistrarEntregaRequest, Result<EntregaDto>>
    {
        private readonly IEntregaService _entregaService = entregaService;

        public async Task<Result<EntregaDto>> Handle(RegistrarEntregaRequest request, CancellationToken cancellationToken)
        {
            return await _entregaService.RegistrarEntregaAsync(request.FornecedorId, request.ProdutoId);
        }
    }
}
