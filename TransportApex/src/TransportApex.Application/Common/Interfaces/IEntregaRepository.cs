using TransportApex.Domain.Entities;

namespace TransportApex.Application.Common.Interfaces
{
    public interface IEntregaRepository
    {
        Task AdicionarAsync(Entrega entrega);
        Task<IEnumerable<Entrega>> ObterTodasAsync();
        Task<Entrega?> ObterPorIdAsync(long id);
    }
}
