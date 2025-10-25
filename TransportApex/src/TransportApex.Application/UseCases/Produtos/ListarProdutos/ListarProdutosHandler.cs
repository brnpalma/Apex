using Apex.Shared.Interfaces;
using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Produtos;

namespace TransportApex.Application.UseCases.Produtos.ListarProdutos
{
    public class ListarProdutosHandler(IProdutoService produtoService, ICurrentUserService currentUserService) 
        : IRequestHandler<ListarProdutosRequest, Result<IEnumerable<ListaProdutoDto>>>
    {
        private readonly IProdutoService _produtoService = produtoService;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<IEnumerable<ListaProdutoDto>>> Handle(ListarProdutosRequest request, CancellationToken cancellationToken)
        {
            return await _produtoService.ListarAsync(_currentUserService.IdUsuario, _currentUserService.Role);
        }
    }
}
