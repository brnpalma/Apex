using Apex.Shared.Enums;
using Apex.Shared.Interfaces;
using System.Security.Claims;

namespace TransportApex.WebApi.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string? IdUsuario => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public Role? Role
        {
            get
            {
                var roleString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);

                if (Enum.TryParse<Role>(roleString, ignoreCase: true, out var roleEnum))
                {
                    return roleEnum;
                }

                return null;
            }
        }
    }
}
