using Apex.Shared.Results;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Produtos;
using TransportApex.Domain.Entities;

namespace TransportApex.Application.UseCases.Entregas.Services
{
    public class ProdutoService(IProdutoRepository produtoRepository) : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public async Task<Result<ProdutoDto>> CadastrarAsync(string descricao, decimal peso)
        {
            if (await _produtoRepository.ExistePorNomeAsync(descricao))
                return Result<ProdutoDto>.Fail(400, "Já existe um produto com este nome.", null);

            if (!Produto.TryCreate(descricao, peso, out var produto, out var error))
            {
                return Result<ProdutoDto>.Fail(400, error ?? "Produto inválido.", null);
            }

            await _produtoRepository.AdicionarAsync(produto);

            var produtoDto = new ProdutoDto
            {
                Id = produto.Id,
                Descricao = produto.Descricao,
                Peso = produto.Peso,
                Mensagem = "Produto cadastrado com sucesso",
                Sucesso = true
            };

            return Result<ProdutoDto>.Ok(produtoDto, 201);
        }

        public async Task<Result<IEnumerable<ListaProdutoDto>>> ListarAsync()
        {
            var produtos = await _produtoRepository.ObterTodosAsync();

            var produtosDto = produtos
                .Select(p => new ListaProdutoDto
                {
                    Id = p.Id,
                    Descricao = p.Descricao,
                    Peso = p.Peso,
                })
                .ToList();

            return Result<IEnumerable<ListaProdutoDto>>.Ok(produtosDto, 200);
        }
    }
}
