using AuthApex.Application.Auth.Commands.CadastrarUsuario;
using AuthApex.Application.Auth.Commands.Login;
using AuthApex.Application.Auth.Dtos;
using AuthApex.Application.Auth.Responses;
using AuthApex.Application.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApex.WebApi.Controllers
{
    [ApiController]
    [Route($"api/{Constantes.ApiVersion}")]
    public class AuthController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [AllowAnonymous]
        [HttpPost("usuarios")]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result<UsuarioDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<UsuarioDto>), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [EndpointDescription("Cria um novo usuário. Recebe dados de cadastro e retorna o usuário criado.")]
        public async Task<IActionResult> Usuarios([FromBody] UsuarioRequest request)
        {
            var result = await _sender.Send(request);
            return StatusCode(result.Status, result.Data is null ? result : result.Data);
        }

        [AllowAnonymous]
        [HttpPost("auth/tokens")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<TokenDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<TokenDto>), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [EndpointDescription("Autentica um usuário e gera um token JWT. Retorna o token e informações básicas da sessão.")]
        public async Task<IActionResult> Tokens([FromBody] TokenRequest request)
        {
            var result = await _sender.Send(request);
            return StatusCode(result.Status, result.Data is null ? result : result.Data);
        }
    }
}
