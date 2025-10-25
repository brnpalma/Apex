using Apex.Shared.Interfaces;
using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Entregas;

namespace TransportApex.Application.UseCases.Entregas.ListarEntregas
{
    public class ListarEntregasHandler(IEntregaService entregaService, ICurrentUserService currentUserService) 
        : IRequestHandler<ListarEntregasRequest, Result<IEnumerable<ListaEntregaDto>>>
    {
        private readonly IEntregaService _entregaService = entregaService;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<IEnumerable<ListaEntregaDto>>> Handle(ListarEntregasRequest request, CancellationToken cancellationToken)
        {
            return await _entregaService.ListarAsync(_currentUserService.IdUsuario, _currentUserService.Role);
        }
    }
}
