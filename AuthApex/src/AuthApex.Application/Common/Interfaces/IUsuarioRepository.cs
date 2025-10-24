using AuthApex.Domain.Entities;

namespace AuthApex.Application.Common.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObterPorEmailAsync(string email);
        Task AdicionarAsync(Usuario usuario);
        Task<bool> ExistePorEmailAsync(string email);
    }
}
