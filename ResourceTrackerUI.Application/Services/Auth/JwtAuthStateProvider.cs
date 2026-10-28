using ResourceTrackerUI.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace ResourceTrackerUI.Application.Services.Auth
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storage;

        public JwtAuthStateProvider(ILocalStorageService storage)
        {
            _storage = storage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _storage.GetItemAsync<string>("token");

            if (string.IsNullOrWhiteSpace(token))
            {
                return Anonymous();
            }

            var user = BuildPrincipal(token);

            if (user is null)
            {
                // Expired or unreadable token - drop it so nothing keeps sending it to the API.
                await _storage.RemoveItemAsync("token");
                return Anonymous();
            }

            return new AuthenticationState(user);
        }

        public async Task MarkUserAsAuthenticatedAsync(string token)
        {
            await _storage.SetItemAsync("token", token);

            var user = BuildPrincipal(token) ?? Anonymous().User;

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOutAsync()
        {
            await _storage.RemoveItemAsync("token");

            NotifyAuthenticationStateChanged(
                Task.FromResult(Anonymous()));
        }

        private static AuthenticationState Anonymous()
        {
            return new AuthenticationState(
                new ClaimsPrincipal(new ClaimsIdentity()));
        }

        /// <summary>
        /// Builds the claims principal from the JWT, or returns null when the token cannot be read
        /// or has already expired. The signature is intentionally not validated here - the API
        /// remains the authority - but a stale token is useless to keep around.
        /// </summary>
        private static ClaimsPrincipal? BuildPrincipal(string jwt)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(jwt);

                if (token.ValidTo != DateTime.MinValue && token.ValidTo <= DateTime.UtcNow)
                {
                    return null;
                }

                var identity = new ClaimsIdentity(token.Claims, "jwt");
                return new ClaimsPrincipal(identity);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
