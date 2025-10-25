using Apex.Shared.Enums;
using AuthApex.Domain.ValueObjects;

namespace AuthApex.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public Senha SenhaHash { get; set; }
        public Email Email { get; set; }
        public Role Role { get; set; }

    }
}
