using TransportApex.Domain.Entities;

namespace TransportApex.Application.Common.Interfaces
{
    public interface IFornecedorRepository
    {
        Task<bool> ExistePorCnpjAsync(string cnpj);
        Task AdicionarAsync(Fornecedor fornecedor);
        Task<Fornecedor?> ObterPorIdAsync(long id);
        Task<IEnumerable<Fornecedor>> ObterTodosAsync();
    }
}
