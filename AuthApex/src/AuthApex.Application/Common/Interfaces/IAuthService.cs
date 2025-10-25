using Apex.Shared.Results;
using AuthApex.Application.Dtos.Autenticacao;

namespace AuthApex.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<Result<UsuarioDto>> CadastrarUsuarioAsync(string email, string senha);
        Task<Result<TokenDto>> LoginAsync(string email, string senha);
    }
}
