using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Dtos.Entregas;

namespace TransportApex.Application.UseCases.Entregas.ConsultarEntregas
{
    public record ConsultarEntregasRequest() : IRequest<Result<IEnumerable<EntregaDto>>>;
}
