using AuthApex.Application.Common.Interfaces;
using AuthApex.Domain.Entities;
using AuthApex.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthApex.Infrastructure.Repositories
{
    public class UsuarioRepository(ApplicationDbContext dbContext) : IUsuarioRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Usuario> ObterPorEmailAsync(string email) =>
            await _dbContext.Usuarios.SingleOrDefaultAsync(u => u.Email.Endereco == email);

        public async Task AdicionarAsync(Usuario usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistePorEmailAsync(string email) =>
            await _dbContext.Usuarios.AnyAsync(u => u.Email.Endereco == email);
    }
}
