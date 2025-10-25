using Apex.Shared.Results;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Entregas;
using TransportApex.Domain.Entities;

namespace TransportApex.Application.UseCases.Entregas.Services
{
    public class EntregaService(IEntregaRepository entregaRepository, IFornecedorRepository fornecedorRepository,
                                IProdutoRepository produtoRepository) : IEntregaService
    {
        private readonly IEntregaRepository _entregaRepository = entregaRepository;
        private readonly IFornecedorRepository _fornecedorRepository = fornecedorRepository;
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public async Task<Result<EntregaDto>> RegistrarEntregaAsync(long fornecedorId, long produtoId)
        {
            var fornecedor = await _fornecedorRepository.ObterPorIdAsync(fornecedorId);

            if (fornecedor is null)
                return Result<EntregaDto>.Fail(404, "Fornecedor não encontrado.", null);

            var produto = await _produtoRepository.ObterPorIdAsync(produtoId);

            if (produto is null)
                return Result<EntregaDto>.Fail(404, "Produto não encontrado.", null);

            var entrega = new Entrega(fornecedor, produto);
            await _entregaRepository.AdicionarAsync(entrega);

            var entregaDto = new EntregaDto
            {
                Id = entrega.Id,
                FornecedorId = fornecedor.Id,
                FornecedorNome = fornecedor.Nome,
                ProdutoId = produto.Id,
                DescricaoProduto = produto.Descricao,
                Mensagem = "Entrega cadastrada com sucesso",
                Sucesso = true
            };

            return Result<EntregaDto>.Ok(entregaDto, 201);
        }

        public async Task<Result<IEnumerable<ListaEntregaDto>>> ListarAsync()
        {
            var entregas = await _entregaRepository.ObterTodasAsync();

            var entregasDto = entregas
                .Select(e => new ListaEntregaDto
                {
                    Id = e.Id,
                    FornecedorId = e.Fornecedor.Id,
                    FornecedorNome = e.Fornecedor.Nome,
                    ProdutoId = e.Produto.Id,
                    DescricaoProduto = e.Produto.Descricao,
                })
                .ToList();

            return Result<IEnumerable<ListaEntregaDto>>.Ok(entregasDto, 200);
        }
    }
}
