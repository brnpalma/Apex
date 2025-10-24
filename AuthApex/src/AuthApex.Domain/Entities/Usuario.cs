using AuthApex.Domain.ValueObjects;

namespace AuthApex.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string PasswordHash { get; set; }
        public Email Email { get; set; }
    }
}
