using Apex.Shared.Enums;
using Apex.Shared.Results;
using TransportApex.Application.Dtos.Entregas;

namespace TransportApex.Application.Common.Interfaces
{
    public interface IEntregaService
    {
        Task<Result<EntregaDto>> RegistrarEntregaAsync(long fornecedorId, long produtoId, string idUsuario, Role? role);
        Task<Result<IEnumerable<ListaEntregaDto>>> ListarAsync(string idUsuario, Role? role);
    }
}
