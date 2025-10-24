namespace AuthApex.Application.Auth.Dtos
{
    public class CadastrarUsuarioResultDto
    {
        public string IdUsuario { get; set; }
        public string Email { get; set; }
        public string Retorno { get; set; }
        public bool Sucesso { get; set; }
    }
}
