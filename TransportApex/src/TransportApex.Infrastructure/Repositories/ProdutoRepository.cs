using Microsoft.EntityFrameworkCore;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Domain.Entities;
using TransportApex.Infrastructure.Persistence;

namespace TransportApex.Infrastructure.Repositories
{
    public class ProdutoRepository(TransportDbContext context) : IProdutoRepository
    {
        private readonly TransportDbContext _context = context;

        public async Task<bool> ExistePorNomeAsync(string descricao)
        {
            return await _context.Produtos.AnyAsync(p => p.Descricao == descricao);
        }

        public async Task AdicionarAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<Produto?> ObterPorIdAsync(long id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }
    }
}
