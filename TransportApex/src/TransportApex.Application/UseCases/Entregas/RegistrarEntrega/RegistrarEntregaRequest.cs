using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Dtos.Entregas;

namespace TransportApex.Application.UseCases.Entregas.RegistrarEntrega
{
    public record RegistrarEntregaRequest(long FornecedorId, long ProdutoId) : IRequest<Result<EntregaDto>>;
}