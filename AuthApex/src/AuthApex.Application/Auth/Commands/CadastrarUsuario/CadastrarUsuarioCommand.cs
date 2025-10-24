using AuthApex.Application.Auth.Dtos;

namespace AuthApex.Application.Auth.Commands.CadastrarUsuario
{
    public record CadastrarUsuarioCommand(string Email, string Senha) : IRequest<CadastrarUsuarioResultDto>;
}
