using Apex.Shared.Enums;

namespace AuthApex.Domain.Entities
{
    public class Token
    {
        public int IdUsuario { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        public string TokenJwt { get; set; }

    }
}
