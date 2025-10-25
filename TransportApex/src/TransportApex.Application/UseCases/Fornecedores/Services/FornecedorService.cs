using Apex.Shared.Results;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Fornecedores;
using TransportApex.Domain.Entities;
using TransportApex.Domain.ValueObjects;

namespace TransportApex.Application.UseCases.Fornecedores.Services
{
    public class FornecedorService(IFornecedorRepository fornecedorRepository) : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository = fornecedorRepository;

        public async Task<Result<FornecedorDto>> CadastrarAsync(string nome, string cnpj)
        {
            if (await _fornecedorRepository.ExistePorCnpjAsync(cnpj))
                return Result<FornecedorDto>.Fail(400, "Já existe um fornecedor com este CNPJ.", null);

            var fornecedor = new Fornecedor(nome, new Cnpj(cnpj));
            await _fornecedorRepository.AdicionarAsync(fornecedor);

            var fornecedorDto = new FornecedorDto
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome,
                Cnpj = fornecedor.Cnpj,
                Mensagem = "Fornecedor cadastrado com sucesso",
                Sucesso = true
            };

            return Result<FornecedorDto>.Ok(fornecedorDto, 201);
        }

        public async Task<Result<IEnumerable<FornecedorDto>>> ListarAsync()
        {
            var fornecedores = await _fornecedorRepository.ObterTodosAsync();

            var fornecedoresDto = fornecedores
                .Select(f => new FornecedorDto 
                {
                    Id = f.Id, 
                    Nome = f.Nome, 
                    Cnpj = f.Cnpj
                })
                .ToList();

            return Result<IEnumerable<FornecedorDto>>.Ok(fornecedoresDto, 200);
        }

        //public async Task<Result<UsuarioDto>> CadastrarUsuarioAsync(string email, string senha)
        //{
        //    if (!Email.TryCreate(email, out var emailVo, out var emailError))
        //    {
        //        return Result<UsuarioDto>.Fail(400, emailError!, null);
        //    }

        //    if (await _fornecedorRepository.ExistePorEmailAsync(emailVo.Endereco))
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

        //    await _fornecedorRepository.AdicionarAsync(usuario);

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
        //    var usuario = await _fornecedorRepository.ObterPorEmailAsync(email);

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
