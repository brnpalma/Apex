using Apex.Shared.Results;
using TransportApex.Application.Dtos.Produtos;

namespace TransportApex.Application.Common.Interfaces
{
    public interface IProdutoService
    {
        Task<Result<ProdutoDto>> CadastrarAsync(string descricao, decimal peso);
        Task<Result<IEnumerable<ListaProdutoDto>>> ListarAsync();
    }
}
