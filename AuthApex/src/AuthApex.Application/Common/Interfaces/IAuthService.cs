using AuthApex.Application.Auth.Dtos;

namespace AuthApex.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<CadastrarUsuarioResultDto> CadastrarUsuarioAsync(string email, string senha);
        Task<LoginResultDto> LoginAsync(string email, string senha);
    }
}
