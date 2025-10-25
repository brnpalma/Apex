using Apex.Shared.Results;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Produtos;
using TransportApex.Domain.Entities;

namespace TransportApex.Application.UseCases.Entregas.Services
{
    public class ProdutoService(IProdutoRepository produtoRepository) : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public async Task<Result<ProdutoDto>> CadastrarAsync(string descricao, decimal peso)
        {
            if (await _produtoRepository.ExistePorNomeAsync(descricao))
                return Result<ProdutoDto>.Fail(400, "Já existe um produto com este nome.", null);

            var produto = new Produto(descricao, peso);
            await _produtoRepository.AdicionarAsync(produto);

            var produtoDto = new ProdutoDto
            {
                Id = produto.Id,
                Descricao = produto.Descricao,
                Peso = produto.Peso,
                Mensagem = "Produto cadastrado com sucesso",
                Sucesso = true
            };

            return Result<ProdutoDto>.Ok(produtoDto, 201);
        }

        public async Task<Result<IEnumerable<ProdutoDto>>> ListarAsync()
        {
            var produtos = await _produtoRepository.ObterTodosAsync();

            var produtosDto = produtos
                .Select(p => new ProdutoDto
                {
                    Id = p.Id,
                    Descricao = p.Descricao,
                    Peso = p.Peso,
                    Sucesso = true
                })
                .ToList();

            return Result<IEnumerable<ProdutoDto>>.Ok(produtosDto, 200);
        }

        //private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        //private readonly ITokenService _tokenService = tokenService;

        //public async Task<Result<UsuarioDto>> CadastrarUsuarioAsync(string email, string senha)
        //{
        //    if (!Email.TryCreate(email, out var emailVo, out var emailError))
        //    {
        //        return Result<UsuarioDto>.Fail(400, emailError!, null);
        //    }

        //    if (await _usuarioRepository.ExistePorEmailAsync(emailVo.Endereco))
        //    {
        //        return Result<UsuarioDto>.Fail(400, "Já existe um usuário com o email informado.", null);
        //    }

        //    var usuario = new Usuario
        //    {
        //        Email = new Email(email),
        //        Role = Role.Admin
        //    };

        //    if (!Senha.TryCreate(senha, out var senhaVo, out var senhaError))
        //    {
        //        return Result<UsuarioDto>.Fail(400, senhaError!, null);
        //    }

        //    usuario.SenhaHash = senhaVo;

        //    await _usuarioRepository.AdicionarAsync(usuario);

        //    var usuarioDto = new UsuarioDto
        //    {
        //        IdUsuario = usuario.Id.ToString(),
        //        Email = usuario.Email.Endereco,
        //        Mensagem = "Usuário cadastrado com sucesso",
        //        Sucesso = true
        //    };

        //    return Result<UsuarioDto>.Ok(usuarioDto, 201);
        //}

        //public async Task<Result<TokenDto>> LoginAsync(string email, string senha)
        //{
        //    var usuario = await _usuarioRepository.ObterPorEmailAsync(email);

        //    if (usuario is null)
        //    {
        //        return Result<TokenDto>.Fail(404, "Usuário não encontrado.", null);
        //    }

        //    var senhaVo = new Senha(usuario.SenhaHash.ValorHash);

        //    if (!senhaVo.Verificar(senha))
        //    {
        //        return Result<TokenDto>.Fail(401, "Senha inválida.", null);
        //    }

        //    var token = new Token
        //    {
        //        IdUsuario = usuario.Id,
        //        Role = usuario.Role,
        //        Email = usuario.Email.Endereco,
        //        TokenJwt = _tokenService.GerarToken(usuario)
        //    };

        //    var tokenDto = new TokenDto
        //    {
        //        Sucesso = true,
        //        Mensagem = "Token gerado com sucesso.",
        //        IdUsuario = usuario.Id,
        //        Role = usuario.Role,
        //        Email = usuario.Email.Endereco,
        //        Token = token.TokenJwt
        //    };

        //    return Result<TokenDto>.Ok(tokenDto, 200);
        //}

        //public Task<bool> ValidarTokenAsync(string token)
        //{
        //    return Task.FromResult(_tokenService.ValidarToken(token));
        //}
    }
}
