using Apex.Shared.Results;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Entregas;
using TransportApex.Domain.Entities;

namespace TransportApex.Application.UseCases.Entregas.Services
{
    public class EntregaService(IEntregaRepository entregaRepository, IFornecedorRepository fornecedorRepository,
                                IProdutoRepository produtoRepository) : IEntregaService
    {
        private readonly IEntregaRepository _entregaRepository = entregaRepository;
        private readonly IFornecedorRepository _fornecedorRepository = fornecedorRepository;
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public async Task<Result<EntregaDto>> RegistrarEntregaAsync(long fornecedorId, long produtoId)
        {
            var fornecedor = await _fornecedorRepository.ObterPorIdAsync(fornecedorId);

            if (fornecedor is null)
                return Result<EntregaDto>.Fail(404, "Fornecedor não encontrado.", null);

            var produto = await _produtoRepository.ObterPorIdAsync(produtoId);

            if (produto is null)
                return Result<EntregaDto>.Fail(404, "Produto não encontrado.", null);

            var entrega = new Entrega(fornecedor, produto);
            await _entregaRepository.AdicionarAsync(entrega);

            var entregaDto = new EntregaDto
            {
                Id = entrega.Id,
                FornecedorId = fornecedor.Id,
                FornecedorNome = fornecedor.Nome,
                ProdutoId = produto.Id,
                DescricaoProduto = produto.Descricao,
                Mensagem = "Entrega cadastrada com sucesso",
                Sucesso = true
            };

            return Result<EntregaDto>.Ok(entregaDto, 201);
        }

        public async Task<Result<IEnumerable<EntregaDto>>> ListarAsync()
        {
            var entregas = await _entregaRepository.ObterTodasAsync();

            var entregasDto = entregas
                .Select(e => new EntregaDto
                {
                    Id = e.Id,
                    FornecedorId = e.Fornecedor.Id,
                    FornecedorNome = e.Fornecedor.Nome,
                    ProdutoId = e.Produto.Id,
                    DescricaoProduto = e.Produto.Descricao,
                    Sucesso = true
                })
                .ToList();

            return Result<IEnumerable<EntregaDto>>.Ok(entregasDto, 200);
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
