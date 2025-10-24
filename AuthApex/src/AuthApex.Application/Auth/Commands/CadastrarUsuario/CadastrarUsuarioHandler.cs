using AuthApex.Application.Auth.Dtos;
using AuthApex.Application.Common.Interfaces;

namespace AuthApex.Application.Auth.Commands.CadastrarUsuario
{
    public class CadastrarUsuarioHandler(IAuthService authService) : IRequestHandler<CadastrarUsuarioCommand, CadastrarUsuarioResultDto>
    {
        private readonly IAuthService _authService = authService;

        public async Task<CadastrarUsuarioResultDto> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await _authService.CadastrarUsuarioAsync(request.Email, request.Senha);
        }
    }
}
