using AuthApex.Application.Auth.Dtos;
using AuthApex.Application.Auth.Responses;
using AuthApex.Application.Common.Interfaces;

namespace AuthApex.Application.Auth.Commands.Login
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
