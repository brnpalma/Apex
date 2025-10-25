using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Dtos.Produtos;

namespace TransportApex.Application.UseCases.Produtos.CadastrarProdutos
{
    public record CadastrarProdutosRequest(string Descricao, decimal Peso) : IRequest<Result<ProdutoDto>>;
}
