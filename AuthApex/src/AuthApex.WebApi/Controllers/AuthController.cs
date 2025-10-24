using AuthApex.Application.Auth.Commands.CadastrarUsuario;
using AuthApex.Application.Auth.Commands.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApex.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        /// <summary>
        /// Cadastro de novo usuário.
        /// </summary>
        /// <param name="command">Dados de registro</param>
        /// <returns>Usuário cadastrado ou mensagem de erro</returns>
        [AllowAnonymous]
        [HttpPost("cadastrar")]
        [EndpointDescription("Cadastro de novo usuário.")]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarUsuarioCommand command)
        {
            var result = await _sender.Send(command);

            if (!result.Sucesso)
            {
                return BadRequest(new { result.Retorno });
            }

            return Ok(result);
        }

        /// <summary>
        /// Login do usuário e geração de token JWT.
        /// </summary>
        /// <param name="command">Credenciais do usuário</param>
        /// <returns>Token JWT</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [EndpointDescription("Login do usuário e geração de token JWT.")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var tokenResult = await _sender.Send(command);

            if (tokenResult is null || string.IsNullOrWhiteSpace(tokenResult.Token))
            {
                return Unauthorized(new { Retorno = "As credenciais informadas são inválidas." });
            }

            return Ok(tokenResult);
        }
    }
}
