using AuthApex.Application.Auth.Dtos;
using AuthApex.Application.Auth.Responses;

namespace AuthApex.Application.Auth.Commands.Login
{
    public record TokenRequest(string Email, string Senha) : IRequest<Result<TokenDto>>;
}
