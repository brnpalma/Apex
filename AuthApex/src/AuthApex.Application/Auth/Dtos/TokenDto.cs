namespace AuthApex.Application.Auth.Dtos
{
    public class TokenDto
    {
        public bool Sucesso { get; set; }
        public int IdUsuario { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Mensagem { get; set; }
    }
}
