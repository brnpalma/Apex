namespace AuthApex.Application.Auth.Dtos
{
    public class UsuarioDto
    {
        public bool Sucesso { get; set; }
        public string? IdUsuario { get; set; } = null;
        public string? Email { get; set; } = null;
        public string Mensagem { get; set; }
    }
}
