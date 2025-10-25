using Apex.Shared.Results;
using AuthApex.Application.Common.Interfaces;
using AuthApex.Application.Dtos.Autenticacao;

namespace AuthApex.Application.UseCases.Autenticacao.CadastrarUsuarios
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
