using AuthApex.Application.Auth.Dtos;
using AuthApex.Application.Common.Interfaces;

namespace AuthApex.Application.Auth.Commands.Login
{
    public class LoginHandler(IAuthService authService) : IRequestHandler<LoginCommand, LoginResultDto>
    {
        private readonly IAuthService _authService = authService;

        public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request.Email, request.Senha);
        }
    }
}
