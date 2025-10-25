using Apex.Shared.Enums;

namespace Apex.Shared.Interfaces
{
    public interface ICurrentUserService
    {
        string? IdUsuario { get; }
        Role? Role { get; }
    }
}
