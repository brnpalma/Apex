using AuthApex.Domain.Entities;

namespace AuthApex.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
        bool ValidarToken(string token);
    }
}
