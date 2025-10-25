using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Dtos.Entregas;

namespace TransportApex.Application.UseCases.Entregas.ListarEntregas
{
    public record ListarEntregasRequest() : IRequest<Result<IEnumerable<ListaEntregaDto>>>;
}
