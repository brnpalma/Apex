using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Dtos.Produtos;

namespace TransportApex.Application.UseCases.Produtos.ListarProdutos
{
    public record ListarProdutosRequest() : IRequest<Result<IEnumerable<ListaProdutoDto>>>;
}
