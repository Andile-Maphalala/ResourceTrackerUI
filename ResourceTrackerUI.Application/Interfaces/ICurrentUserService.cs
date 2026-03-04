

using System.Security.Claims;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface ICurrentUserService
    {
        Task<ClaimsPrincipal> GetUserAsync();
        Task<string> GetUserNameAsync();
        Task<string> GetEmailAsync();
        Task<string[]> GetRolesAsync();
        Task<int> GetUserIdAsync();
        Task<bool> GetUserAuthenticaticationStatus();
    }
}
