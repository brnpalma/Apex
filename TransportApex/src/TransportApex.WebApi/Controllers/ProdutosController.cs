﻿using Apex.Shared.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransportApex.Application.Common.Constants;
using TransportApex.Application.Dtos.Produtos;
using TransportApex.Application.UseCases.Produtos.CadastrarProdutos;
using TransportApex.Application.UseCases.Produtos.ListarProdutos;

namespace TransportApex.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route($"api/{ConstantesTransport.ApiVersion}")]
    public class ProdutosController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [Authorize]
        [HttpPost("produtos")]
        [ProducesResponseType(typeof(ProdutoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result<ProdutoDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<ProdutoDto>), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [EndpointDescription("Cria um novo usuário. Recebe dados de cadastro e retorna o usuário criado.")]
        public async Task<IActionResult> CadastrarProdutos([FromBody] CadastrarProdutosRequest request)
        {
            var result = await _sender.Send(request);
            return StatusCode(result.Status, result.Data is null ? result : result.Data);
        }

        [Authorize]
        [HttpGet("produtos")]
        [ProducesResponseType(typeof(ProdutoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<ProdutoDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<ProdutoDto>), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [EndpointDescription("Autentica um usuário e gera um token JWT. Retorna o token e informações básicas da sessão.")]
        public async Task<IActionResult> ListarProdutos()
        {
            var result = await _sender.Send(new ListarProdutosRequest());

            if (result.Data is null)
                return StatusCode(result.Status, result);

            return result.Data.Any()
                ? StatusCode(result.Status, result.Data)
                : Ok(new { Mensagem = "Nenhum produto cadastrado." });
        }
    }
}
