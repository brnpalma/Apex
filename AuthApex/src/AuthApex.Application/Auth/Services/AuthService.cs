using AuthApex.Application.Auth.Dtos;
using AuthApex.Application.Common.Interfaces;
using AuthApex.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthApex.Application.Auth.Services
{
    public class AuthService(IUsuarioRepository usuarioRepository,
                       IPasswordHasher<Usuario> passwordHasher,
                       ITokenService tokenService) : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IPasswordHasher<Usuario> _passwordHasher = passwordHasher;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<CadastrarUsuarioResultDto> CadastrarUsuarioAsync(string email, string senha)
        {
            if (await _usuarioRepository.ExistePorEmailAsync(email))
                throw new Exception("Usuário já existe");

            var usuario = new Usuario { Email = email };
            usuario.PasswordHash = _passwordHasher.HashPassword(usuario, senha);

            await _usuarioRepository.AdicionarAsync(usuario);

            return new CadastrarUsuarioResultDto
            {
                IdUsuario = usuario.Id.ToString(),
                Email = usuario.Email
            };
        }

        public async Task<LoginResultDto> LoginAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(email) 
                ?? throw new ArgumentException("Usuário não encontrado");

            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, senha);

            var token = _tokenService.GerarToken(usuario);

            return new LoginResultDto
            {
                Token = token,
                Email = usuario.Email
            };
        }

        public Task<bool> ValidarTokenAsync(string token)
        {
            return Task.FromResult(_tokenService.ValidarToken(token));
        }
    }

}
