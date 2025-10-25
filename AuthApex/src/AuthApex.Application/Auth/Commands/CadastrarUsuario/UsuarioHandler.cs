using AuthApex.Application.Auth.Dtos;
using AuthApex.Application.Auth.Responses;
using AuthApex.Application.Common.Interfaces;

namespace AuthApex.Application.Auth.Commands.CadastrarUsuario
{
    public class UsuarioHandler(IAuthService authService) : IRequestHandler<UsuarioRequest, Result<UsuarioDto>>
    {
        private readonly IAuthService _authService = authService;

        public async Task<Result<UsuarioDto>> Handle(UsuarioRequest request, CancellationToken cancellationToken)
        {
            return await _authService.CadastrarUsuarioAsync(request.Email, request.Senha);
        }
    }
}
