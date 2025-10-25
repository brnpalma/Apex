using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Entregas;

namespace TransportApex.Application.UseCases.Entregas.ListarEntregas
{
    public class ListarEntregasHandler(IEntregaService entregaService) : IRequestHandler<ListarEntregasRequest, Result<IEnumerable<ListaEntregaDto>>>
    {
        private readonly IEntregaService _entregaService = entregaService;

        public async Task<Result<IEnumerable<ListaEntregaDto>>> Handle(ListarEntregasRequest request, CancellationToken cancellationToken)
        {
            return await _entregaService.ListarAsync();
        }
    }
}
