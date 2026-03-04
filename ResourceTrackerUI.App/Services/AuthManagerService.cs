using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ResourceTrackerUI.App.Services
{
    public class AuthManagerService
    {
        private readonly ResourceTrackerApiClient _api;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthManagerService(ResourceTrackerApiClient api, IHttpContextAccessor httpContextAccessor)
        {
            _api = api;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SignIn(LoginRequestModel request)
        {
            var dto = new LoginRequest
            {
                Email = request.Username,
                Password = request.Password
            };

            var response = await _api.ApiAccountLoginAsync(dto);

            // Parse JWT to extract claims and roles
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(response.Token);

            var claims = jwt.Claims
                .Where(c => c.Type != "exp" && c.Type != "nbf" && c.Type != "iat")
                .Select(c => new Claim(c.Type, c.Value))
                .ToList();

            // Ensure standard name/identifier claims exist
            if (!claims.Any(c => c.Type == ClaimTypes.Name))
            {
                claims.Add(new Claim(ClaimTypes.Name, response.UserName ?? string.Empty));
            }
            if (!claims.Any(c => c.Type == ClaimTypes.NameIdentifier))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, response.Id.ToString()));
            }
            if (!claims.Any(c => c.Type == ClaimTypes.Email) && !string.IsNullOrEmpty(response.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, response.Email));
            }

            // Persist the access token so the DelegatingHandler can pick it up
            claims.Add(new Claim("access_token", response.Token));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var context = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("No active HttpContext");

            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new Microsoft.AspNetCore.Authentication.AuthenticationProperties { IsPersistent = false });
        }

        public Task SignOutAsync()
        {
            return _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
