using TransportApex.Domain.Entities;

namespace TransportApex.Application.Common.Interfaces
{
    public interface IProdutoRepository
    {
        Task<bool> ExistePorNomeAsync(string descricao);
        Task AdicionarAsync(Produto produto);
        Task<Produto?> ObterPorIdAsync(long id);
        Task<IEnumerable<Produto>> ObterTodosAsync();
    }
}
