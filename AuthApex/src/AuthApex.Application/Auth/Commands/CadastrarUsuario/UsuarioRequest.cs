using AuthApex.Application.Auth.Dtos;
using AuthApex.Application.Auth.Responses;

namespace AuthApex.Application.Auth.Commands.CadastrarUsuario
{
    public record UsuarioRequest(string Email, string Senha) : IRequest<Result<UsuarioDto>>;
}
