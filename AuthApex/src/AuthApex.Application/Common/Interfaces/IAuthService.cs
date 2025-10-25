using AuthApex.Application.Auth.Dtos;
using AuthApex.Application.Auth.Responses;

namespace AuthApex.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<Result<UsuarioDto>> CadastrarUsuarioAsync(string email, string senha);
        Task<Result<TokenDto>> LoginAsync(string email, string senha);
    }
}
