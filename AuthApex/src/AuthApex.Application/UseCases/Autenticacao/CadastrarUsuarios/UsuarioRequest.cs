using Apex.Shared.Results;
using AuthApex.Application.Dtos.Autenticacao;

namespace AuthApex.Application.UseCases.Autenticacao.CadastrarUsuarios
{
    public record UsuarioRequest(string Email, string Senha) : IRequest<Result<UsuarioDto>>;
}
