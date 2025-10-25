using Apex.Shared.Results;
using AuthApex.Application.Common.Interfaces;
using AuthApex.Application.Dtos.Autenticacao;

namespace AuthApex.Application.UseCases.Autenticacao.GerarToken
{
    public class TokenHandler(IAuthService authService) : IRequestHandler<TokenRequest, Result<TokenDto>>
    {
        private readonly IAuthService _authService = authService;

        public async Task<Result<TokenDto>> Handle(TokenRequest request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request.Email, request.Senha);
        }
    }
}
