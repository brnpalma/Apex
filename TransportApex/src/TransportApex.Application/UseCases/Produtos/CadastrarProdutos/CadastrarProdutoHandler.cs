using Apex.Shared.Interfaces;
using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Produtos;

namespace TransportApex.Application.UseCases.Produtos.CadastrarProdutos
{
    public class CadastrarProdutoHandler(IProdutoService authService, ICurrentUserService currentUserService) 
        : IRequestHandler<CadastrarProdutosRequest, Result<ProdutoDto>>
    {
        private readonly IProdutoService _produtoService = authService;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<ProdutoDto>> Handle(CadastrarProdutosRequest request, CancellationToken cancellationToken)
        {
            return await _produtoService.CadastrarAsync(request.Descricao, request.Peso, 
                _currentUserService.IdUsuario, _currentUserService.Role);
        }
    }
}
