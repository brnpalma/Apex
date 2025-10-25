using Apex.Shared.Enums;

namespace AuthApex.Application.Dtos.Autenticacao
{
    public class TokenDto
    {
        public bool Sucesso { get; set; }
        public int IdUsuario { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Mensagem { get; set; }
    }
}
