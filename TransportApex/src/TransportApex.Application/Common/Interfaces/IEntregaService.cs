using Apex.Shared.Results;
using TransportApex.Application.Dtos.Entregas;

namespace TransportApex.Application.Common.Interfaces
{
    public interface IEntregaService
    {
        Task<Result<EntregaDto>> RegistrarEntregaAsync(long fornecedorId, long produtoId);
        Task<Result<IEnumerable<ListaEntregaDto>>> ListarAsync();
    }
}
