using Microsoft.EntityFrameworkCore;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Domain.Entities;
using TransportApex.Infrastructure.Persistence;

namespace TransportApex.Infrastructure.Repositories
{
    public class EntregaRepository(TransportDbContext context) : IEntregaRepository
    {
        private readonly TransportDbContext _context = context;

        public async Task AdicionarAsync(Entrega entrega)
        {
            _context.Entregas.Add(entrega);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Entrega>> ObterTodasAsync()
        {
            return await _context.Entregas
                .Include(e => e.Fornecedor)
                .Include(e => e.Produto)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Entrega?> ObterPorIdAsync(long id)
        {
            return await _context.Entregas
                .Include(e => e.Fornecedor)
                .Include(e => e.Produto)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
