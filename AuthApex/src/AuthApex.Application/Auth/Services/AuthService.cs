using AuthApex.Application.Auth.Dtos;
using AuthApex.Application.Auth.Responses;
using AuthApex.Application.Common.Interfaces;
using AuthApex.Domain.Entities;
using AuthApex.Domain.Enums;
using AuthApex.Domain.ValueObjects;

namespace AuthApex.Application.Auth.Services
{
    public class AuthService(IUsuarioRepository usuarioRepository, ITokenService tokenService) : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<Result<UsuarioDto>> CadastrarUsuarioAsync(string email, string senha)
        {
            if (!Email.TryCreate(email, out var emailVo, out var emailError))
            {
                return Result<UsuarioDto>.Fail(400, emailError!, null);
            }

            if (await _usuarioRepository.ExistePorEmailAsync(emailVo.Endereco))
            {
                return Result<UsuarioDto>.Fail(400, "Já existe um usuário com o email informado.", null);
            }

            var usuario = new Usuario
            {
                Email = new Email(email),
                Role = Role.Admin.ToString()
            };

            if (!Senha.TryCreate(senha, out var senhaVo, out var senhaError))
            {
                return Result<UsuarioDto>.Fail(400, senhaError!, null);
            }

            usuario.SenhaHash = senhaVo;

            await _usuarioRepository.AdicionarAsync(usuario);

            var usuarioDto = new UsuarioDto
            {
                IdUsuario = usuario.Id.ToString(),
                Email = usuario.Email.Endereco,
                Mensagem = "Usuário cadastrado com sucesso",
                Sucesso = true
            };

            return Result<UsuarioDto>.Ok(usuarioDto, 201);
        }

        public async Task<Result<TokenDto>> LoginAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(email);

            if (usuario is null)
            {
                return Result<TokenDto>.Fail(404, "Usuário não encontrado.", null);
            }

            var senhaVo = new Senha(usuario.SenhaHash.ValorHash);

            if (!senhaVo.Verificar(senha))
            {
                return Result<TokenDto>.Fail(401, "Senha inválida.", null);
            }

            var token = _tokenService.GerarToken(usuario);

            var tokenDto = new TokenDto
            {
                Sucesso = true,
                Mensagem = "Token gerado com sucesso.",
                IdUsuario = usuario.Id,
                Role = usuario.Role,
                Email = usuario.Email.Endereco,
                Token = token
            };

            return Result<TokenDto>.Ok(tokenDto, 200);
        }

        public Task<bool> ValidarTokenAsync(string token)
        {
            return Task.FromResult(_tokenService.ValidarToken(token));
        }
    }

}
