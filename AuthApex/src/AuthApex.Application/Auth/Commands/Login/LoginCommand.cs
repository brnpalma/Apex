using AuthApex.Application.Auth.Dtos;

namespace AuthApex.Application.Auth.Commands.Login
{
    public record LoginCommand(string Email, string Senha) : IRequest<LoginResultDto>;
}
