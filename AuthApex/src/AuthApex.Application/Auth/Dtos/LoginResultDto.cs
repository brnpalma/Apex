namespace AuthApex.Application.Auth.Dtos
{
    public class LoginResultDto
    {
        public int IdUsuario { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
