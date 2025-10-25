using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Produtos;

namespace TransportApex.Application.UseCases.Produtos.ListarProdutos
{
    public class ListarProdutosHandler(IProdutoService produtoService) : IRequestHandler<ListarProdutosRequest, Result<IEnumerable<ListaProdutoDto>>>
    {
        private readonly IProdutoService _produtoService = produtoService;

        public async Task<Result<IEnumerable<ListaProdutoDto>>> Handle(ListarProdutosRequest request, CancellationToken cancellationToken)
        {
            return await _produtoService.ListarAsync();
        }
    }
}
