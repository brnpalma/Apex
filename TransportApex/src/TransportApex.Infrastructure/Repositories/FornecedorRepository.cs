using Microsoft.EntityFrameworkCore;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Domain.Entities;
using TransportApex.Infrastructure.Persistence;

namespace TransportApex.Infrastructure.Repositories
{
    public class FornecedorRepository(TransportDbContext context) : IFornecedorRepository
    {
        private readonly TransportDbContext _context = context;

        public async Task<bool> ExistePorCnpjAsync(string cnpj)
        {
            return await _context.Fornecedores.AnyAsync(f => f.Cnpj.Numero == cnpj);
        }

        public async Task AdicionarAsync(Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();
        }

        public async Task<Fornecedor?> ObterPorIdAsync(long id)
        {
            return await _context.Fornecedores.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Fornecedor>> ObterTodosAsync()
        {
            return await _context.Fornecedores.AsNoTracking().ToListAsync();
        }
    }
}
