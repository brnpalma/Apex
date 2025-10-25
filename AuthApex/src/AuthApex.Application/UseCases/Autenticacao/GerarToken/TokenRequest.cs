using Apex.Shared.Results;
using AuthApex.Application.Dtos.Autenticacao;

namespace AuthApex.Application.UseCases.Autenticacao.GerarToken
{
    public record TokenRequest(string Email, string Senha) : IRequest<Result<TokenDto>>;
}
