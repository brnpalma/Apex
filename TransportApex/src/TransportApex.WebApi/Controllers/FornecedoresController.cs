using Apex.Shared.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransportApex.Application.Common.Constants;
using TransportApex.Application.Dtos.Fornecedores;
using TransportApex.Application.UseCases.Fornecedores.CadastrarFornecedores;
using TransportApex.Application.UseCases.Fornecedores.ListarFornecedores;

namespace TransportApex.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route($"api/{ConstantesTransport.ApiVersion}")]
    public class FornecedoresController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [Authorize]
        [HttpPost("fornecedores")]
        [ProducesResponseType(typeof(FornecedorDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result<FornecedorDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<FornecedorDto>), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [EndpointDescription("Cria um novo usuário. Recebe dados de cadastro e retorna o usuário criado.")]
        public async Task<IActionResult> CadastrarFornecedor([FromBody] CadastrarFornecedoresRequest request)
        {
            var result = await _sender.Send(request);
            return StatusCode(result.Status, result.Data is null ? result : result.Data);
        }

        [Authorize]
        [HttpGet("fornecedores")]
        [ProducesResponseType(typeof(FornecedorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<FornecedorDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<FornecedorDto>), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [EndpointDescription("Autentica um usuário e gera um token JWT. Retorna o token e informações básicas da sessão.")]
        public async Task<IActionResult> ListarFornecedores()
        {
            var result = await _sender.Send(new ListarFornecedoresRequest());

            if (result.Data is null)
                return StatusCode(result.Status, result);

            return result.Data.Any()
                ? StatusCode(result.Status, result.Data)
                : Ok(new { Mensagem = "Nenhum fornecedor cadastrado." });
        }
    }
}
