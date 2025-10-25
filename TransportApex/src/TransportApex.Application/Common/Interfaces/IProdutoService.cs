using Apex.Shared.Enums;
using Apex.Shared.Results;
using TransportApex.Application.Dtos.Produtos;

namespace TransportApex.Application.Common.Interfaces
{
    public interface IProdutoService
    {
        Task<Result<ProdutoDto>> CadastrarAsync(string descricao, decimal peso, string idUsuario, Role? role);
        Task<Result<IEnumerable<ListaProdutoDto>>> ListarAsync(string idUsuario, Role? role);
    }
}
