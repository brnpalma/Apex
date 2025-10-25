using Apex.Shared.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransportApex.Application.Common.Constants;
using TransportApex.Application.Dtos.Entregas;
using TransportApex.Application.UseCases.Entregas.ListarEntregas;
using TransportApex.Application.UseCases.Entregas.RegistrarEntrega;

namespace TransportApex.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route($"api/{ConstantesTransport.ApiVersion}")]
    public class EntregasController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [Authorize]
        [HttpPost("entregas")]
        [ProducesResponseType(typeof(EntregaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result<EntregaDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<EntregaDto>), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [EndpointDescription("Cria um novo usuário. Recebe dados de cadastro e retorna o usuário criado.")]
        public async Task<IActionResult> RegistrarEntrega([FromBody] RegistrarEntregaRequest request)
        {
            var result = await _sender.Send(request);
            return StatusCode(result.Status, result.Data is null ? result : result.Data);
        }

        [Authorize]
        [HttpGet("entregas")]
        [ProducesResponseType(typeof(EntregaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<EntregaDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<EntregaDto>), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [EndpointDescription("Retorna a lista de entregas cadastradas.")]
        public async Task<IActionResult> ListarEntregas()
        {
            var result = await _sender.Send(new ListarEntregasRequest());

            if (result.Data is null)
                return StatusCode(result.Status, result);

            return result.Data.Any()
                ? StatusCode(result.Status, result.Data)
                : Ok(new { Mensagem = "Nenhuma entrega cadastrada." });
        }
    }
}
