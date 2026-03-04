
using Microsoft.AspNetCore.Components.Authorization;
using ResourceTrackerUI.Application.Interfaces;
using System.Security.Claims;

namespace ResourceTrackerUI.Application.Services.Auth
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly AuthenticationStateProvider _authProvider;

        public CurrentUserService(AuthenticationStateProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public async Task<ClaimsPrincipal> GetUserAsync()
        {
            var state = await _authProvider.GetAuthenticationStateAsync();
            return state.User;
        }

        public async Task<string> GetUserNameAsync()
        {
            var user = await GetUserAsync();
            return user.Claims.FirstOrDefault(c => c.Type == "sub")?.Value ?? "Unknown";
        }

        public async Task<string> GetEmailAsync()
        {
            var user = await GetUserAsync();
            return user.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        }

        public async Task<string[]> GetRolesAsync()
        {
            var user = await GetUserAsync();
            return user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();
        }

        public async Task<int> GetUserIdAsync()
        {
            var user = await GetUserAsync();
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "uid");
            if(userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }

        public async Task<bool> GetUserAuthenticaticationStatus()
        {
            var user = await GetUserAsync();
            if (user != null && user.Identity != null)
            {
                return user.Identity.IsAuthenticated;
            }

            return false;
        }
    }
}
